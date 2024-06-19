<!DOCTYPE html>
<html lang="en">
<head>
  <meta charset="UTF-8">
  <meta http-equiv="X-UA-Compatible" content="IE=edge">
  <meta name="viewport" content="width=device-width, initial-scale=1.0">
  <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/boxicons@latest/css/boxicons.min.css">
  <link rel="stylesheet" href="https://unpkg.com/boxicons@latest/css/boxicons.min.css">
  <link rel="preconnect" href="https://fonts.googleapis.com">
  <link rel="preconnect" href="https://fonts.gstatic.com" crossorigin>
  <link href="https://fonts.googleapis.com/css2?family=Poppins:wght@300;400;500;600;700;800;900&display=swap" rel="stylesheet">
  <link rel="stylesheet" href="produtos.css">

  <title>Produtos || Dribe Modas</title>
</head>
<body>
  <header class="header">
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

  <div class="container">
    <h1 class="h1">Catálogo de Produtos</h1>
    <div class="catalogo">
      <?php
      require 'conexao.php';
      
      $produtos_por_pagina = 6;
      $pagina_atual = isset($_GET['pagina']) ? (int)$_GET['pagina'] : 1;
      $inicio = ($pagina_atual - 1) * $produtos_por_pagina;
      
      $sql = "SELECT produto.id, produto.nome, produto.descricao,produto.marca, produto.preco, produto.tipo, produto.tamanho, produto.imagem, fornecedor.id AS id_fornecedor 
              FROM Produto_Fornecedor
              JOIN fornecedor  ON Produto_Fornecedor.id_fornecedor = fornecedor.id
              JOIN produto on produto.id = Produto_Fornecedor.id_produto
              GROUP BY produto.id
              LIMIT $inicio, $produtos_por_pagina";
      $result = $conn->query($sql);
      
      if ($result->num_rows > 0) {
        while($row = $result->fetch_assoc()) {
          echo "<div class='produto'>";
          if (!empty($row['imagem'])) {
            echo "<img src='data:image/jpeg;base64," . base64_encode($row['imagem']) . "' alt='" . $row['nome'] . "' />";
          } else {
            echo "<p>Sem imagem disponível</p>";
          }
          echo "<div class='detalhes'>";
          echo "<h3>" . $row['nome'] . "</h3>";
          echo "<div class='tamanhos'>";
          $tamanhos = explode(',', $row['tamanho']);
          foreach ($tamanhos as $tamanho) {
            echo "<button class='tamanho'>" . $tamanho . "</button>";
          }
          echo "</div>";
          echo "<p class='preco'>R$ " . number_format($row['preco'], 2, ',', '.') . "</p>";
          echo "<form method='post' action='pedido.php'>";
          echo "<input type='hidden' name='id_produto' value='" . $row['id'] . "'>";
          echo "<input type='hidden' name='id_fornecedor' value='" . $row['id_fornecedor'] . "'>";
          echo "<label for='quantidade'>Quantidade:</label>";
          echo "<input type='number' name='quantidade' min='1' value='1' required>";
          echo "<button type='submit' class='add-to-cart'><i class='bx bx-cart-add'></i> Fazer pedido</button>";
          echo "</form>";
          echo "</div>";
          echo "</div>";
        }
        
        $sql_total = "SELECT COUNT(DISTINCT produto.id) AS total FROM Produto_Fornecedor JOIN produto ON produto.id = Produto_Fornecedor.id_produto";
        $resultado_total = $conn->query($sql_total);
        $total_produtos = $resultado_total->fetch_assoc()['total'];
        $total_paginas = ceil($total_produtos / $produtos_por_pagina);
        
        echo "<div class='pagination'>";
        for ($i = 1; $i <= $total_paginas; $i++) {
          echo "<a href='produtos.php?pagina=$i' " . ($pagina_atual == $i ? "class='active'" : "") . ">$i</a>";
        }
        echo "</div>";
        
      } else {
        echo "<p>Nenhum produto encontrado.</p>";
      }
      
      $conn->close();
      ?>
    </div>
  </div>
</body>
</html>
