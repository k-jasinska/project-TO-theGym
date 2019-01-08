<?php
require_once('baza.php');
session_start();
function filtruj($zmienna,$link)
{
    if (get_magic_quotes_gpc()) {
        $zmienna = stripslashes($zmienna);
    }
$zmienna=htmlspecialchars(trim($zmienna));
    return mysqli_real_escape_string($link,$zmienna);
}
if(!(($_SESSION['zalogowany'])==1) || !isset($_SESSION['zalogowany'])){
    header("Location: index.php?error=0");
    exit();
}

if(!empty($_POST))
{
    $output = '';
      $name = filtruj($_POST['name'],$link);
      $surname = filtruj($_POST['surname'],$link);
      $email = filtruj($_POST['email'],$link);
      $phone = filtruj($_POST['phone'],$link);
      $street = filtruj($_POST['street'],$link);
      $code = filtruj($_POST['code'],$link);
      $city = filtruj($_POST['city'],$link);

      if(empty($name) || empty($surname) || empty($email)|| empty($phone)|| empty($street)|| empty($code) || empty($city) ) {
        $_SESSION['e_len']="Musisz wypełnić wszystkie pola";
        exit("2");
    }

    if((strlen($name)<3) || (strlen($name)>20)){
      $_SESSION['e_len']="Imię musi mieć od 3 do 20 znaków";
      exit("2");
  }
  if((strlen($surname)<3) || (strlen($surname)>80)){
    $_SESSION['e_len']="Nazwisko musi mieć od 3 do 80 znaków";
    exit("2");
}
if(strlen($email)>25){
    $_SESSION['e_len']="Email nie może mieć więcej niż 25 znaków";
    exit("2");
}
if((strlen($phone)<9) || (strlen($phone)>9)){
    $_SESSION['e_len']="Nr telefonu musi mieć 9 znaków";
    exit("2"); 
}

   
 
       
  if (mysqli_num_rows(mysqli_query($link,"SELECT email FROM Person WHERE email = '" . $email . "';")) == 0) 
  {
    $pass_hash=password_hash($name, PASSWORD_DEFAULT);
    mysqli_query($link, "INSERT INTO adres (street, code, city) VALUES ('$street', '$code', '$city');");
    $insertID=mysqli_insert_id($link);
    mysqli_query($link, "insert into Person (name, surname, email, password, phone, id_adres, id_P_Type) values ('$name','$surname','$email','$pass_hash','$phone',$insertID,2);");
    $query=1;
  }
  else{
    $query=0;
    $_SESSION['e_len']="Email jest zajety!";
    exit("2");
    // echo("email jest zajety!");
  }
    if($query)
    {
      // $output .= '<label class="text-success">Dodano klienta!</label>';
     $output .= '
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
     ';
 
     if(isset($_GET['Did']))
     {
     $_GET['Did']=mysqli_real_escape_string($link, $_GET['Did']);
     mysqli_query($link, "delete from Person where id_person='$_GET[Did]' limit 1;");
     }
     $q=mysqli_query($link, "Select * from Person");
     while($tabl=mysqli_fetch_assoc($q)){
     $tabl['name']=htmlspecialchars($tabl['name']);
     $tabl['surname']=htmlspecialchars($tabl['surname']);
     $tabl['email']=htmlspecialchars($tabl['email']);
     $tabl['phone']=htmlspecialchars($tabl['phone']);
     $tabl['id_adres']=htmlspecialchars($tabl['id_adres']);



     $output .= ' 
     <tr> 
     
     <td>' . $tabl["id_person"] . ' </td> 
     <td>' . $tabl["name"] . '</td>
     <td> ' . $tabl["surname"] . '</td>
     <td> ' . $tabl["email"] . '</td>
     <td> ' . $tabl["phone"] . '</td>
     <td><a href="?Did=' . $tabl["id_person"] . '">usuń</a></td>
    <td><input type="button" name="view" value="view" id="' . $tabl["id_adres"] . '" class="btn btn-info btn-xs view_data" /></td>  
      </tr>
      ';
     }

     $output .= '
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
     ';
    }
     echo $output;
}
?>  