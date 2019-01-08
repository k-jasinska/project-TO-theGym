<?php
require_once('baza.php');
session_start();

?> 

<!DOCTYPE html>
<html lang="pl">
<head>
<title>TheGym</title>
  <meta charset="utf-8">
  <link rel="shortcut icon" href="style/image/favicon .ico" />
  
  <meta name="viewport" content="width=device-width, initial-scale=1">
  <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css">
<script type="text/javascript" src="http://interceptedby.admuncher.com/B8E9874D11E639F3/helper.js#0.180149.0" id="wXWx_MainScript"></script><link rel="stylesheet" href="http://interceptedby.admuncher.com/B8E9874D11E639F3/helper.css" type="text/css" media="all" />
<script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>
  <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>

 <link rel="stylesheet" type="text/css" href="style/styleRegister.css" /> 
 
 <script>
$(document).ready(function(){
  $('#email').focusout(function(){
        var email = $(this).val();
        $.post("infoRejestracja.php", {email:email}, function(result){
             $("#info").html(result);
        }).fail(function(){
             alert("error");
        });
});
});
</script>
</head>

<body>

 <?php
 echo '<pre>';
 if(isset($_GET['error']))
 {
         switch($_GET['error']){
                 case 0: echo 'Zarejestruj sie do systemu'; break;
         }
 }
 echo '</pre>';
 ?>
  
<div class="container">
    <div class="row">
  
            <form action="zarejestruj.php" class="qwe" method="post">
            <h1>Zarejestruj się!</h1>
                 <div class="col-sm-6">
           
                <div class="form-group">
                <label for="name">Imię:</label>
                <input type="text" class="form-control" id="name" maxlength="20" placeholder="Wprowadź imię" name="name" value="<?php if(isset($_SESSION['fr_name'])){ echo $_SESSION['fr_name']; unset($_SESSION['fr_name']);} ?>">
                </div>
                <?php
                if(isset($_SESSION['e_name']))
                {
                        echo '<div class="info">'.$_SESSION['e_name'].'</div>';
                        unset($_SESSION['e_name']);
                }
                ?>
                <div class="form-group">
                <label for="surname">Nazwisko:</label>
                <input type="text" class="form-control" id="surname" maxlength="80" placeholder="Wprowadź nazwisko" name="surname" value="<?php if(isset($_SESSION['fr_surname'])){ echo $_SESSION['fr_surname']; unset($_SESSION['fr_surname']);} ?>">
                </div>
                <?php
                if(isset($_SESSION['e_surname']))
                {
                        echo '<div class="info">'.$_SESSION['e_surname'].'</div>';
                        unset($_SESSION['e_surname']);
                }
                ?>
                <div class="form-group">
                <label for="email">Email:</label>
                <input type="email" class="form-control" maxlength="25" id="email" placeholder="Twój email" name="email" value="<?php if(isset($_SESSION['fr_email'])){ echo $_SESSION['fr_email']; unset($_SESSION['fr_email']);} ?>">
                </div>
                <div id="info"> </div>
                <?php
                if(isset($_SESSION['e_email']))
                {
                        echo '<div class="info">'.$_SESSION['e_email'].'</div>';
                        unset($_SESSION['e_email']);
                }
                ?>
                <div class="form-group">
                <label for="pwd">Hasło:</label>
                <input type="password" class="form-control" id="pwd" maxlength="20" placeholder="Twoje hasło" name="password" value="<?php if(isset($_SESSION['fr_password'])){ echo $_SESSION['fr_password']; unset($_SESSION['fr_password']);} ?>">
                </div>
                <?php
                if(isset($_SESSION['e_pass']))
                {
                        echo '<div class="info">'.$_SESSION['e_pass'].'</div>';
                        unset($_SESSION['e_pass']);
                }
                ?>
                  <div class="form-group">
                <label for="phone">Nr telefonu:</label>
                <input type="phone" class="form-control" maxlength="9" id="phone" placeholder="Nr telefonu" name="phone" value="<?php if(isset($_SESSION['fr_phone'])){ echo $_SESSION['fr_phone']; unset($_SESSION['fr_phone']);} ?>">
                </div>
                <?php
                if(isset($_SESSION['e_phone']))
                {
                        echo '<div class="info">'.$_SESSION['e_phone'].'</div>';
                        unset($_SESSION['e_phone']);
                }
                ?>

        </div>


    <div class="col-sm-6">
                <div class="form-group">
                            <label for="street">Ulica:</label>
                            <input type="street" class="form-control" maxlength="15" id="street" placeholder="Ulica" name="street" value="<?php if(isset($_SESSION['fr_street'])){ echo $_SESSION['fr_street']; unset($_SESSION['fr_street']);} ?>">
                </div>  
                <?php
                if(isset($_SESSION['e_street']))
                {
                        echo '<div class="info">'.$_SESSION['e_street'].'</div>';
                        unset($_SESSION['e_street']);
                }
                ?>
                <div class="form-group">
                            <label for="postalcode">Kod pocztowy:</label>
                            <input type="postalcode" maxlength="6" class="form-control" id="postalcode" placeholder="Kod pocztowy" name="code" value="<?php if(isset($_SESSION['fr_code'])){ echo $_SESSION['fr_code']; unset($_SESSION['fr_code']);} ?>">
                </div>  
                <?php
                if(isset($_SESSION['e_code']))
                {
                        echo '<div class="info">'.$_SESSION['e_code'].'</div>';
                        unset($_SESSION['e_code']);
                }
                ?>
                <div class="form-group">
                            <label for="city">Miasto:</label>
                            <input type="city" class="form-control" maxlength="15" id="city" placeholder="Miasto:" name="city" value="<?php if(isset($_SESSION['fr_city'])){ echo $_SESSION['fr_city']; unset($_SESSION['fr_city']);} ?>">
                </div>
                <?php
                if(isset($_SESSION['e_city']))
                {
                        echo '<div class="info">'.$_SESSION['e_city'].'</div>';
                        unset($_SESSION['e_city']);
                }
                ?>
                <div class="form-group form-check">
                <label class="form-check-label">
                <input class="form-check-input" type="checkbox" name="check1" id="check1" <?php if(isset($_SESSION['fr_chech1'])){echo "checked"; unset($_SESSION['fr_check1']);}?> > Wyrażam zgodę na przetwarzanie moich danych osobowych.
                </label>
                </div>

        <div class="form-group form-check">
            <label class="form-check-label">
            <input class="form-check-input" type="checkbox" name="check2" id="check2" <?php if(isset($_SESSION['fr_chech2'])){echo "checked"; unset($_SESSION['fr_check2']);}?> > Oświadczam, iż zapoznałem się z treścią  <a href="https://pl.wikipedia.org/wiki/Regulamin" data-toggle="tooltip" title="Czytaj regulamin">regulaminu</a> i akceptuje jego wszystkie postanowienia. Akceptuje Politykę Ochrony Prywatności-ogólne standardy ochrony.
            </label>
            </div>

            <?php
                if(isset($_SESSION['e_len']))
                {
                        echo '<div class="info">'.$_SESSION['e_len'].'</div>';
                        unset($_SESSION['e_len']);
                }
                ?>

             <button type="submit" class="btn btn-info" name="rejestruj">Zarejestruj się</button>
            </div>
        </form>
    </div>
</div>



<div class=" cookie alert alert-dark" role="alert">
    <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>
  <strong>Ta strona wykorzystuje pliki cookie!</strong> Korzystając z niej wyrażasz zgodę na ich używanie.
  <!-- <button type="button" class="close" data-dismiss="alert" aria-label="Close"> -->
    <!-- <span aria-hidden="true">&times;</span> -->
  <!-- </button> -->
</div> 


</body>
</html>

