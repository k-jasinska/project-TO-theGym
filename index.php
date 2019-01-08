<?php
require_once('baza.php');
 session_start();
    if(isset($_SESSION['zalogowany']) && $_SESSION['zalogowany']==1){
        header("Location: AdminPanel.php");
        exit();
    }
    else if(isset($_SESSION['zalogowany']) && $_SESSION['zalogowany']==2){
        header("Location: user.php");
        exit();
    }
?>  

<!DOCTYPE html>
<html lang="pl">
<head>
  <meta charset="utf-8">
  <title>TheGym</title>
  <link rel="shortcut icon" href="style/image/favicon .ico" />
  <meta name="viewport" content="width=device-width, initial-scale=1">

  <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.1.3/css/bootstrap.min.css">
  <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>
  <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.14.3/umd/popper.min.js"></script>
  <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.1.3/js/bootstrap.min.js"></script>
   <link rel="stylesheet" type="text/css" href="style/styleLogin.css" />
</head>
<body>

<?php
 echo '<pre>';
 if(isset($_GET['error']))
 {
         switch($_GET['error']){
                 case 0: echo 'Musisz sie zalogować'; break;
                 case 1: echo 'Zostałeś wylogowany'; break;
                 case 2: echo 'Konto zostało utworzone. Zaloguj się!'; break;

         }
 }
 echo '</pre>';
 ?>

<div class=bg>
    <div class="row">
            <div class="col-sm-8 col-12 log">
                    <div class="container">
                    <form action="index.php" method="post">
                    <h2>Formularz logowania</h2>
                            <div class="form-group">
                            <label for="email">Email:</label>
                            <input type="email" class="form-control" id="email" placeholder="Wpisz email" name="email" value="<?php if(isset($_COOKIE["email"])) {echo $_COOKIE["email"];}?>">
                            </div>
                            <div class="form-group">
                            <label for="password">Password:</label>
                            <input type="password" class="form-control" id="pswd" placeholder="Podaj hasło" name="pswd" value="<?php if(isset($_COOKIE["password"])) {echo $_COOKIE["password"];}?>">
                            </div>
                            <div class="form-group form-check">
                            <label class="form-check-label">
                            <input class="form-check-input" type="checkbox" name="remember" id="remember" <?php if(isset($_COOKIE["email"])) { ?> checked <?php } ?> > Zapamiętaj mnie
                            </label>
                            </div>
                            <button type="button" class="btn btn-info" id="log_in" name="log_in">Zaloguj</button>
                           <div id="info"></div>
                            </form>
                    </div>
            </div>
            <div class="col-sm-4 col-12">
                <p>Nie masz jeszcze konta? </p>
                <a href='rejestracja.php'>
             <button type="submit" class="btn btn-info"  name='rejestruj' id="register">Zarejestruj się</button>
            </a>
            </div>
    </div>
</div>

<!-- <div id="cookie">popup</div> -->
<br>
<div class=" cookie alert alert-dark alert-dismissible fade show" role="alert">
  <strong>Ta strona wykorzystuje pliki cookie!</strong> Korzystając z niej wyrażasz zgodę na ich używanie.
  <button type="button" class="close" data-dismiss="alert" aria-label="Close">
    <span aria-hidden="true">&times;</span>
  </button>
</div>

<!-- <script>
function setCookie(name, val, time){
    if(time){
        var data=new Date();
        data.setTime(data.getTime() +(time*24*60*60*1000));
        var expires="; expires="+data.toGMTString();
    } else{
        var expires="";
    }
    document.cookie=name+ "="+val+ expires+"; path=/";
}

$(document).ready(function(){
if(document.cookie.indexOf("cookie")>0){
    setTimeout(function(){
        $('#cookie').fadeIn(300);
    }, 3000); 
    
    //    setCookie("cookie", "1", 1);
}
});
</script> -->
 
<script>
$(document).ready(function(){
    $("#log_in").click(function(){
        var email = $("#email").val();
        var pswd = $("#pswd").val();
        var remember = $("#remember").val();
        $.post("zaloguj.php", {log_in:1, emailPHP:email,passwordPHP:pswd, rememberPHP:remember}, function(result){
           if(result==1)
           {
            location.replace("AdminPanel.php");
           }
           else if(result==2)
           {
            location.replace("user.php");
           }
           else{
            $("#info").html(result);
           }
            
        });
    });
});
</script>


<noscript>
      <pre> Twoja przeglądarna nie obsługuje języka JavaScript. Zmień ustawienia, aby korzystać z aplikacji.</pre>
    </noscript>
</body>
</html>