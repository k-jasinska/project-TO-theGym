<!-- calke z zadanym przedziale calkowania prze uzytkownika metoda trapezów -->
<?php

// ZAD1
// if(isset($_POST['ok'])){
//     $x1=$_POST['x1'];
//     $x2=$_POST['x2'];
//     $h=$_POST['h'];
//     $sum=0;
//     for($i=$x1;$i<$x2;$i+$h){
//         $a=sin($x1);
//         $b=sin($x1+$h);
//         $sum+=(($a+$b)*$h)/2;
//     }
//     echo "$sum";
// }

// ZAD2

// if(isset($_POST['ok'])){
    //     $a=$_POST['x1'];
    //     $b=$_POST['x1'];
    //     $n=$_POST['n'];

    //     $dx=$b-$a;

    //     $y=rand_float(-1,1);
    //     $x=rand_float(a,b);

    //     for($i=1;$i<=$n;$i++){
    //         // if(<sin()){
    //             $wynik++;
    //         }
    //     }
    //     $wynik=$dx*wynik/$n;
    //    echo "w$ynik";
// }



 if(isset($_POST['ok'])){
     require_once('baza');
     $txt=$_POST['txt'];
     $wynik=mysqli_query($link, "insert into osoba_type(type) value('$txt' );") or die("nie udało się pobrać danych");
 }
?>




<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <meta http-equiv="X-UA-Compatible" content="ie=edge">
    <title>Document</title>
</head>
<body>
    <form action="l1.php" method="post">
    <label for="x">label:</label>
    <input type="text" name="x"><br>
    <textarea name="txt" id="txt" cols="50" rows="10"></textarea>
    <!-- <label for="x1">x1:</label>
    <input type="text" id="x1" name="x1"><br>
    <label for="x2">x2:</label>
    <input type="text" id="x2" name="x2"><br> -->
    <!-- <label for="h">h:</label>
    <input type="text" id="h" name="h"> -->
    <!-- <label for="n">n:</label>
    <input type="text" id="n" name="n"> -->
    <button name="ok">ok</button>
    </form>
</body>
</html>