<?php
session_start();

if(!(($_SESSION['zalogowany'])==1) || !isset($_SESSION['zalogowany'])){
    header("Location: index.php?error=0");
    exit();
}
?>

<?php
require_once('baza.php');
?> 


<!DOCTYPE html>
<html lang="pl">
<head>
  <meta charset="utf-8">
  <title>AdminPanel</title>
  <link rel="shortcut icon" href="style/image/favicon .ico" />
  <meta name="viewport" content="width=device-width, initial-scale=1">

  <!-- icons -->
  <link rel="stylesheet" href="https://use.fontawesome.com/releases/v5.5.0/css/all.css" integrity="sha384-B4dIYHKNBt8Bc12p+WXckhzcICo0wtJAoU8YZTY5qE0Id1GSseTk6S+L3BlXeVIU" crossorigin="anonymous">

  <!-- modal i datatable-->
  <script type="text/javascript" src="https://code.jquery.com/jquery-3.3.1.js"></script>
<script type="text/javascript" src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
<script type="text/javascript" src="https://cdn.datatables.net/1.10.19/js/jquery.dataTables.min.js"></script>
<script type="text/javascript" src="https://cdn.datatables.net/1.10.19/js/dataTables.bootstrap.min.js"></script>
<script type="text/javascript" src="https://cdn.datatables.net/responsive/2.2.3/js/dataTables.responsive.min.js"></script>
<script type="text/javascript" src="https://cdn.datatables.net/responsive/2.2.3/js/responsive.bootstrap.min.js"></script>
<link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css">
<link rel="stylesheet" href="https://cdn.datatables.net/1.10.19/css/dataTables.bootstrap.min.css">
<link rel="stylesheet" href="https://cdn.datatables.net/responsive/2.2.3/css/responsive.bootstrap.min.css">
 <!-- style -->
  <link rel="stylesheet" type="text/css" href="style/styleAdmin.css" /> 
</head>

<body>
<!-- header -->
<div class="wrapper max">
<header>
  <nav id="topnav">
    <div class="logo">GymFit</div>
    <div class="menu1">
    <ul>
        <li>Witaj, Admin  <i class="far fa-user"></i></li>
        <li><a href="logout.php" class="logout">Wyloguj  <i class="fas fa-sign-out-alt "></i></a></li>
      </ul>
    </div>
  </nav>
</header> 
</div>


<!-- sidenav -->
<div class="wrapper1 active"> 
    <div class="burger">
        <i class="fas fa-bars  visible show active "></i>
        <i class="fas fa-times visible active"></i>
    </div>

    <aside class="active">
        		<nav id="ml-menu" class="menu">
			
			<div class="menu__wrap">
				<ul data-menu="main" class="menu__level">
          <li class="menu__item"><a class="menu__link" data-submenu="submenu-1" href="AdminPanel.php"><i class="fas fa-users"></i>Lista klientów</a></li>
					<li class="menu__item"><a class="menu__link" data-submenu="submenu-2" href="CarnetList.php"><i class="fas fa-list-ul"></i>Lista karnetów</a></li>
					<li class="menu__item"><a class="menu__link" data-submenu="submenu-3" href="#"><i class="fas fa-chart-line"></i>Statystyki </a></li>
					<li class="menu__item"><a class="menu__link" data-submenu="submenu-4" href="#"><i class="fas fa-cog"></i>Ustawienia </a></li>		
					</ul>
			</div>
		</nav>
    </aside>
</div>
    

<!-- table -->
<div class="wrapper1 active"> 
    <div class="content">
<button type="button" name="add" id="add" class="btn add-user-btn btn-info" data-toggle="modal" data-target="#myModal">Dodaj klienta</button>
<hr>
<div id="employee_table">
<table id="table" class="table table-striped table-bordered nowrap" style="width:100%; background-color:white;">
      <thead>
      <tr>
      <th >ID</th>
      <th >Imię</th>
      <th >Nazwisko</th>
      <th >E-mail</th>
      <th>Telefon</th>
      <th>Akcja</th>
      <th>Szczegóły</th>
      </tr>
      </thead>
      <tbody>

      <?php

      if(isset($_GET['Did']))
      {
      $_GET['Did']=mysqli_real_escape_string($link, $_GET['Did']);
    
        $res=mysqli_query($link, "delete from Person where id_person='$_GET[Did]' limit 1;");
      if (mysqli_error($link)) {
        echo '<div class="info">'.mysqli_error($link).'</div>';
      }
      }
      $q=mysqli_query($link, "Select * from Person");
      while($tabl=mysqli_fetch_assoc($q)){
      $tabl['name']=htmlspecialchars($tabl['name']);
      $tabl['surname']=htmlspecialchars($tabl['surname']);
      $tabl['email']=htmlspecialchars($tabl['email']);
      $tabl['phone']=htmlspecialchars($tabl['phone']);
      $tabl['id_adres']=htmlspecialchars($tabl['id_adres']);



      echo "<tr> <td>$tabl[id_person] </td> <td>$tabl[name]</td><td> $tabl[surname]</td><td> $tabl[email]</td><td> $tabl[phone]</td> <td><a href='?Did=$tabl[id_person]'>usuń</a></td>
 <td><input type='button' name='$tabl[id_person]' value='view' id='$tabl[id_adres]' class='btn btn-info btn-xs view_data'/></td>
       </tr>";
      }
      ?>
      </tbody>
      <tfoot>
      <tr>
      <th>ID</th>
      <th>Imię</th>
      <th>Nazwisko</th>
      <th>E-mail</th>
      <th>Telefon</th>
      <th>Akcja</th>
      <th>Szczegóły</th>
      </tr>
      </tfoot> 
      </table>
      </div>
</div>
</div>


<!-- modal add user-->
<div class="modal fade" id="myModal">
    <div class="modal-dialog modal-dialog-centered">
      <div class="modal-content">
      
        <!-- Modal Header -->
        <div class="modal-header">
        <button type="button" class="close" data-dismiss="modal">&times;</button>
          <h4 class="modal-title">Dodaj klienta</h4>
        </div>
        
        <!-- Modal body -->
        <div class="modal-body">
        <form role="form" method="post" id="insert_form" >
        <div class="form-group">
                <label for="name">Imię:</label>
                <input type="text" class="form-control" id="name" placeholder="Wprowadź imię" name="name">
                </div>
                <div class="form-group">
                <label for="surname">Nazwisko:</label>
                <input type="text" class="form-control" id="surname" placeholder="Wprowadź nazwisko" name="surname">
                </div>
                <div class="form-group">
                <label for="email">Email:</label>
                <input type="email" class="form-control" id="email" placeholder="Twój email" name="email">
                </div>
                  <div class="form-group">
                <label for="phone">Nr telefonu:</label>
                <input type="phone" class="form-control" id="phone" placeholder="Nr telefonu" name="phone">
                </div>
              <div class="form-group">
              <label for="street">Ulica:</label>
                <input type="street" class="form-control" id="street" placeholder="Ulica" name="street">
                </div>  

                <div class="form-group">
              <label for="postalcode">Nr domu:</label>
              <input type="postalcode" class="form-control" id="postalcode" placeholder="Nr domu" name="code">
                </div>  

                <div class="form-group">
                <label for="city">Miasto:</label>
                <input type="city" class="form-control" id="city" placeholder="Miasto" name="city" >
                </div>

                <div id="err"></div>
                <?php
                // if(isset($_SESSION['e_len']))
                // {
                //         echo '<div class="info">'.$_SESSION['e_len'].'</div>';
                //         unset($_SESSION['e_len']);
                // }
                ?>
              <input type="submit" name="insert" id="insert" value="Dodaj" class="btn btn-success" />
          <button type="button" class="btn btn-danger" data-dismiss="modal">Anuluj</button>
   
        </form>
        </div>
      </div>
    </div>
  </div>



<!--modal details -->
<div id="dataModal" class="modal fade">
 <div class="modal-dialog modal-dialog-centered">
  <div class="modal-content">
   <div class="modal-header">
    <button type="button" class="close" data-dismiss="modal">&times;</button>
    <h4 class="modal-title">Dane adresowe</h4>
   </div>
    <div class="modal-body" id="employee_detail">
    
   </div> 
   <div class="modal-footer">
   <button type="button" class="btn btn-danger" data-dismiss="modal">OK</button>
   </div>
  </div>
 </div>
</div>

 
<!-- add modal -->
<script>  
$(document).ready(function(){
 $('#insert_form').on("submit", function(event){  
  event.preventDefault();  
   $.ajax({  
    url:"dodaj_klienta.php",  
    method:"POST",  
    data:$('#insert_form').serialize(), 
    beforeSend:function(){  
     $('#insert').val("Dodawanie");  
    },  
    success:function(data){ 
      if(data==2) {
        var res= "<?php if(isset($_SESSION['e_len']))  echo $_SESSION['e_len']; ?>" ;
        $('#err').html(res);
        $('#insert').val("Dodaj"); 
        <?php unset($_SESSION['e_len']); ?>
      }
      else{
      $('#insert_form')[0].reset();  
      $('#myModal').modal('hide');
      $('#employee_table').html(data);
      }
    }  
   });  
   
 });
});
</script>

<!-- view details -->
<script>
 $(document).on('click', '.view_data', function(){
  var adres_id = $(this).attr("id");
  var id_person = $(this).attr("name");

  $.ajax({
   url:"view_details.php",
   method:"POST",
   data:{adres_id:adres_id, id_person:id_person},
   success:function(data){
    $('#employee_detail').html(data);
    $('#dataModal').modal('show');
   }
  });
 });

 </script>




<!--zamkniecie polaczenia-->
 <?php
  mysqli_free_result($q);
  mysqli_close($link);
 ?>
<!--   do tabeli-->
<script>$(document).ready(function() {
    $('#table').DataTable( {
        responsive: true
        
    } );
} );</script>

<!--do przyciemnienia naglówka-->
    <script type="text/javascript">
      $(document).ready(function() {
            $(".menu-icon").on("click", function() {
                  $("nav ul").toggleClass("showing");
            });
      });
      $(window).on("scroll", function() {
            if($(window).scrollTop()) {
                  $('#topnav').addClass('black');
            }
            else {
                  $('#topnav').removeClass('black');
            }
      })
      </script>
  
<!--  do chowania nawigacji -->
    <script src="script.js"></script> 


</body>
</html>
