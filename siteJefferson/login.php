<!DOCTYPE html>
<html lang="en">

<head>
  <meta charset="UTF-8">
  <meta http-equiv="X-UA-Compatible" content="IE=edge">
  <meta name="viewport" content="width=device-width, initial-scale=1.0">
  <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/boxicons@latest/css/boxicons.min.css">
  <!-- or -->
  <link rel="stylesheet" href="https://unpkg.com/boxicons@latest/css/boxicons.min.css">
  <link rel="preconnect" href="https://fonts.googleapis.com">
  <link rel="preconnect" href="https://fonts.gstatic.com" crossorigin>
  <link href="https://fonts.googleapis.com/css2?family=Poppins:wght@300;400;500;600;700;800;900&display=swap"
    rel="stylesheet">
  <link rel="stylesheet" href="login.css">

  <title>Fones de Ouvidos</title>
</head>

<body>

  <header>
    <a href="#" class="logo"><i class='bx bx-closet'></i>Dribe Modas</a>

    <ul class="navegação">
            <li><a href="index.html">Inicio</a></li>
            <li><a href="produtos.php">Produtos</a></li>
            <li><a href="#">Sobre</a></li>
            <li><a href="registro.php">Registro</a></li>
            <li><a href="login.php">Login</a></li>
        </ul>

    <div class="header-icons">
      <i class='bx bx-cart'></i>
    </div>
  </header>


  <div class="login">
    <form action="tratarLogin.php" method="post">
      <div class="inputbox">
        <h2>Login</h2>
        <input required="required" type="text" name="email" required>
        <span>Email:</span>
        <i></i>
      </div>
      <div class="inputbox">
        <input required="required" type="password" name="senha" required>
        <span>Senha:</span>
        <i></i>
        
      </div>
      <button type="submit" class="shadow__btn">
        Entrar
      </button>
    </div>
  </form>
    
  <script>

  </script>
</body>

</html>