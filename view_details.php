<?php
if(isset($_POST["adres_id"]))
{
 $output = '';
require_once('baza.php');
 $query = "SELECT * FROM adres WHERE id_adres = '".$_POST["adres_id"]."'";
 $result = mysqli_query($link, $query);
 $output .= '  
      <div class="table-responsive">  
           <table class="table table-bordered">';
    while($row = mysqli_fetch_array($result))
    {
     $output .= '
     <tr>  
            <td width="30%"><label>Ulica</label></td>  
            <td width="70%">'.$row["street"].'</td>  
        </tr>
        <tr>  
            <td width="30%"><label>Nr domu</label></td>  
            <td width="70%">'.$row["code"].'</td>  
        </tr>
        <tr>  
            <td width="30%"><label>Miejscowość</label></td>  
            <td width="70%">'.$row["city"].'</td>  
        </tr>
     ';
    }
    $output .= '</table></div>';

    $output .= '<br><p>Historia wejść:</p>';
    $query = "call p_schow_dateLog('$_POST[id_person]')";
    $result = mysqli_query($link, $query);
    $output .= '  
         <div class="table-responsive">  
              <table class="table table-bordered">';
       while($row = mysqli_fetch_array($result))
       {
        $output .= '
        <tr>   
               <td width="100%">'.$row["date_log"].'</td>  
           </tr>
        ';
       }
       $output .= '</table></div>';


    echo $output;
}
?>