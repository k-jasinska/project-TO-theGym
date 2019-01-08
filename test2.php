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
  <!-- table -->
  <script type="text/javascript" src="https://code.jquery.com/jquery-3.3.1.js"></script>
  <script type="text/javascript" src="https://cdn.datatables.net/1.10.19/js/jquery.dataTables.min.js"></script>
  <script type="text/javascript" src="https://cdn.datatables.net/1.10.19/js/dataTables.foundation.min.js"></script>
  <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/foundation/6.4.3/css/foundation.min.css">
  <link rel="stylesheet" href="https://cdn.datatables.net/1.10.19/css/dataTables.foundation.min.css">
  <!-- icon -->
  <link rel="stylesheet" href="https://use.fontawesome.com/releases/v5.5.0/css/all.css" integrity="sha384-B4dIYHKNBt8Bc12p+WXckhzcICo0wtJAoU8YZTY5qE0Id1GSseTk6S+L3BlXeVIU" crossorigin="anonymous">
<!-- style -->
 <link rel="stylesheet" type="text/css" href="style/styleAdmin.css" /> 
</head>

<body>
      <!-- header -->
<div class="wrapper max">
<header>
  <nav id="topnav">
    <div class="logo">LOGO</div>
    <div class="menu1">
    <ul>
        <li>Witaj, Admin  <i class="far fa-user"></i></li>
        <li><a href="#">Wyloguj  <i class="fas fa-sign-out-alt"></i></a></li>
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
				
                        <li class="menu__item"><a class="menu__link" data-submenu="submenu-1" href="#"><i class="fas fa-users"></i>Lista klientów</a></li>
					<li class="menu__item"><a class="menu__link" data-submenu="submenu-2" href="#"><i class="fas fa-list-ul"></i>Lista karnetów</a></li>
					<li class="menu__item"><a class="menu__link" data-submenu="submenu-3" href="#"><i class="fas fa-chart-line"></i>Statystyki </a></li>
					<li class="menu__item"><a class="menu__link" data-submenu="submenu-4" href="#"><i class="fas fa-cog"></i>Ustawienia </a></li>		
					</ul>
			</div>
		</nav>
    </aside>
</div>
    



<!--content table-->
<div class="wrapper1 active"> 
    <div class="content">
      <table id="example" class="display" style="width:100%">
      <thead>
      <tr>
      <th>ID</th>
      <th>data początku</th>
      <th>data końca</th>
      <th>id osoby</th>
      <th>id typu karnetu</th>
      <th>akcja </th>
      </tr>
      </thead>
      <tbody>

      <?php

      if(isset($_GET['id']))
      {
      $_GET['id']=mysqli_real_escape_string($link, $_GET['id']);
      mysqli_query($link, "delete from Entrance where id_entrance='$_GET[id]' limit 1;");
      }
      $q=mysqli_query($link, "Select * from Entrance;");
      while($tabl=mysqli_fetch_assoc($q)){
      $tabl['begin_date']=htmlspecialchars($tabl['begin_date']);
      $tabl['end_date']=htmlspecialchars($tabl['end_date']);
      $tabl['id_person']=htmlspecialchars($tabl['id_person']);
      $tabl['id_type']=htmlspecialchars($tabl['id_type']);


      echo "<tr>  <td>$tabl[id_entrance]</td> <td>$tabl[begin_date]</td><td> $tabl[end_date]</td><td> $tabl[id_person]</td><td> $tabl[id_type]</td><td><a href='?id=$tabl[id_person]'>usuń</a></td></tr>";
      }
      ?>
      </tbody>
      <tfoot>
      <tr>
      <th>ID</th>
      <th>data początku</th>
      <th>data końca</th>
      <th>id osoby</th>
      <th>id typu karnetu</th>
      <th>akcja </th>
      </tr>
      </tfoot> 
      </table>
      </div>
      





<!--zamkniecie polaczenia-->
 <?php
  mysqli_free_result($q);
  mysqli_close($link);
 ?>
<!--   do tabeli-->
    <script>
  $(document).ready(function() {
    $('#example').DataTable();
} );
    </script>

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
