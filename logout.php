<?php
session_start();
    unset($_SESSION['zalogowany']);
    session_destroy();
    header("Location: index.php?error=1");

?>