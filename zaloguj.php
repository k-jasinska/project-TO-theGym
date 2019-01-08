<?php
require_once('baza.php');
session_start();
  
function filtruj($link, $zmienna)
{
    if (get_magic_quotes_gpc()) {
        $zmienna = stripslashes($zmienna);
    }
$zmienna=htmlspecialchars(trim($zmienna));
    return mysqli_real_escape_string($link,$zmienna);
}

if(isset($_POST['log_in']))
{
      $email=$_POST['emailPHP'];
      $password=$_POST['passwordPHP'];
      $email = filtruj($link, $email);

    if( empty($email) || empty($password)) {
        exit ("Wszystkie pola sa wymagane");
    }
    if((strlen($password)<6) || (strlen($password)>20)){
        exit ("Hasło musi zawierać od 6 do 20 znaków");
    }

   
    mysqli_multi_query($link, "select fcheck_email('$email');") or die("nie udało się pobrać danych");
     if($result1=mysqli_store_result($link))
     {
        $rows1=mysqli_fetch_row($result1);
        // echo $rows1[0];
     }
    if($rows1[0])
    {
        $wynik=mysqli_query($link, "Select * from Person where email='$email';") or die("nie udało się pobrać danych");
        $rows=mysqli_fetch_array($wynik);

        if(($email==$rows['email']) && (password_verify($password, $rows['password']))){
            if(isset($_POST["rememberPHP"])){
                setcookie('email', $email, time()+(1*60*60));
                setcookie('password', $password, time()+(1*60*60));
            }
            else{
                unset($_COOKIE['email']);
                unset($_COOKIE['password']);
                setcookie('email', '', time()-100);
                setcookie('password', '', time()-100);
            }

            if($rows['id_P_Type']==1){
                $_SESSION['zalogowany']=1;
                $_SESSION['user']=$rows['name'];
                exit("1");
            }
            else{
                $_SESSION['zalogowany']=2;
                $_SESSION['id_person']=htmlspecialchars($rows['id_person']);
                $_SESSION['user']=htmlspecialchars($rows['name']);
                $_SESSION['surname']=htmlspecialchars($rows['surname']);
                $_SESSION['phone']=htmlspecialchars($rows['phone']);
                $_SESSION['email']=htmlspecialchars($rows['email']);

                $wynik2=mysqli_query($link, "call find_adress(". $rows['id_adres']. ")") or die("nie udało się pobrać danych adresowych");
                $rows2=mysqli_fetch_array($wynik2);
                $_SESSION['street']=$rows2['street'];
                $_SESSION['code']=$rows2['code'];
                $_SESSION['city']=$rows2['city'];

                exit("2");
            }
        }
        else{
            exit("Podano błędne dane");
        }
}
else{
    exit("Podano zły email");
}
    


}
    else{
        header("Location: index.php");
        // exit("Musisz się zalogować");
    }
?>