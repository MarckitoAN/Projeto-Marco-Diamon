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
  <link rel="stylesheet" href="registro.css">

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

  <section class="home">
    <h2>Cadastre-se</h2>
    <form action="tratativaDeDados.php" method="post">

      <div class="inputbox">
        <input required="required" type="text" name="nome">
        <span>Nome:</span>
        <i></i>
      </div>
      <div class="inputbox">
        <input required="required" type="text" name="rg">
        <span>Rg:</span>
        <i></i>
      </div>
      <div class="inputbox">
        <input required="required" type="text" name="cpf">
        <span>Cpf:</span>
        <i></i>
      </div>
      <div class="inputbox">
        <input required="required" type="tel" name="tel">
        <span>Telefone:</span>
        <i></i>
      </div>
      <div class="inputbox">
        <input required="required" type="text" name="rua">
        <span>Rua:</span>
        <i></i>
      </div>
      <div class="inputbox">
        <input required="required" type="text" name="bairro">
        <span>Bairro:</span>
        <i></i>
      </div>
      <div class="inputbox">
        <input required="required" type="text" name="cidade">
        <span>Cidade:</span>
        <i></i>
      </div>
      <div class="inputbox">
        <input required="required" type="text" name="estado">
        <span>Estado:</span>
        <i></i>
      </div>
      <div class="inputbox">
        <input required="required" type="text" name="email">
        <span>Email:</span>
        <i></i>
      </div>
      <div class="inputbox">
        <input required="required" type="password" name="senha">
        <span>Senha:</span>
        <i></i>
      </div>

      </div>

      <button type="submit" class="c-button c-button--gooey"> Cadastrar
        <div class="c-button__blobs">
          <div></div>
          <div></div>
          <div></div>
        </div>
      </button>
      <svg xmlns="http://www.w3.org/2000/svg" version="1.1" style="display: block; height: 0; width: 0;">
        <defs>
          <filter id="goo">
            <feGaussianBlur in="SourceGraphic" stdDeviation="10" result="blur"></feGaussianBlur>
            <feColorMatrix in="blur" mode="matrix" values="1 0 0 0 0  0 1 0 0 0  0 0 1 0 0  0 0 0 18 -7" result="goo">
            </feColorMatrix>
            <feBlend in="SourceGraphic" in2="goo"></feBlend>
          </filter>
        </defs>
      </svg>
    </form>
  </section>

  <script>

  </script>
</body>

</html>