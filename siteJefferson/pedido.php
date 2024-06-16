<!DOCTYPE html>
<html lang="en">
<head>
  <meta charset="UTF-8">
  <meta name="viewport" content="width=device-width, initial-scale=1.0">
  <title>Detalhes do Pedido</title>
  <link rel="stylesheet" href="pedidos.css">
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
    <h1 class="h1">Detalhes do Pedido</h1>
    <div class="detalhes-pedido">
      <?php
      session_start();
      require 'conexao.php'; // Arquivo de conexão com o banco de dados

      // Verifica se o usuário está logado
      if (!isset($_SESSION['user_id'])) {
        echo "<p>Você não está logado.</p>";
        exit;
      }

      // Processamento do formulário de envio do pedido
      if ($_SERVER["REQUEST_METHOD"] == "POST" && isset($_POST['id_produto'])) {
        $id_cliente = $_SESSION['user_id']; // ID do cliente logado
        $id_produto = $_POST['id_produto'];
        $quantidade = $_POST['quantidade'];
        $id_fornecedor = $_POST['id_fornecedor'];

        // Iniciar transação para garantir a consistência dos dados
        $conn->begin_transaction();

        try {
          // Inserir o pedido na tabela Pedido
          $sql_pedido = "INSERT INTO Pedido (data, id_user, id_cliente, forma_pagamento, parcelas, valor_total)
                         VALUES (NOW(), 1, ?, 'Debito', 1, 0)";
          $stmt_pedido = $conn->prepare($sql_pedido);
          $stmt_pedido->bind_param("i", $id_cliente);
          $stmt_pedido->execute();

          if ($stmt_pedido->affected_rows <= 0) {
            throw new Exception("Falha ao inserir pedido.");
          }

          $pedido_id = $stmt_pedido->insert_id;

          // Inserir o produto associado ao pedido na tabela Pedido_Produto
          $sql_pedido_produto = "INSERT INTO Pedido_Produto (id_pedido, id_produto, id_fornecedor, quantidade)
                                 VALUES (?, ?, ?, ?)";
          $stmt_pedido_produto = $conn->prepare($sql_pedido_produto);
          $stmt_pedido_produto->bind_param("iiii", $pedido_id, $id_produto, $id_fornecedor, $quantidade);
          $stmt_pedido_produto->execute();

          if ($stmt_pedido_produto->affected_rows <= 0) {
            throw new Exception("Falha ao inserir produto no pedido.");
          }

          // Atualizar o valor total do pedido com base no preço do produto e quantidade
          $sql_produto = "SELECT nome, preco FROM Produto WHERE id = ?";
          $stmt_produto = $conn->prepare($sql_produto);
          $stmt_produto->bind_param("i", $id_produto);
          $stmt_produto->execute();
          $result_produto = $stmt_produto->get_result();

          if ($result_produto->num_rows > 0) {
            $produto = $result_produto->fetch_assoc();
            $nome_produto = $produto['nome'];
            $preco_unitario = $produto['preco'];
            $valor_total_produto = $quantidade * $preco_unitario;

            $sql_atualiza_valor_total = "UPDATE Pedido SET valor_total = ? WHERE id = ?";
            $stmt_atualiza_valor_total = $conn->prepare($sql_atualiza_valor_total);
            $stmt_atualiza_valor_total->bind_param("di", $valor_total_produto, $pedido_id);
            $stmt_atualiza_valor_total->execute();
          } else {
            throw new Exception("Produto não encontrado.");
          }

          // Recuperar o nome do fornecedor
          $sql_fornecedor = "SELECT nome FROM fornecedor WHERE id = ?";
          $stmt_fornecedor = $conn->prepare($sql_fornecedor);
          $stmt_fornecedor->bind_param("i", $id_fornecedor);
          $stmt_fornecedor->execute();
          $result_fornecedor = $stmt_fornecedor->get_result();

          if ($result_fornecedor->num_rows > 0) {
            $nome_fornecedor = $result_fornecedor->fetch_assoc()['nome'];
          } else {
            throw new Exception("Fornecedor não encontrado.");
          }

          // Recuperar o nome do cliente
          $sql_cliente = "SELECT nome FROM cliente WHERE id = ?";
          $stmt_cliente = $conn->prepare($sql_cliente);
          $stmt_cliente->bind_param("i", $id_cliente);
          $stmt_cliente->execute();
          $result_cliente = $stmt_cliente->get_result();

          if ($result_cliente->num_rows > 0) {
            $nome_cliente = $result_cliente->fetch_assoc()['nome'];
          } else {
            throw new Exception("Cliente não encontrado.");
          }

          // Exibir os detalhes do pedido
          echo "<p>Nome do Produto: $nome_produto</p>";
          echo "<p>Nome do Fornecedor: $nome_fornecedor</p>";
          echo "<p>Valor Total: R$ " . number_format($valor_total_produto, 2, ',', '.') . "</p>";
          echo "<p>Nome do Cliente: $nome_cliente</p>";

          // Commit da transação
          $conn->commit();

        } catch (Exception $e) {
          // Rollback da transação em caso de erro
          $conn->rollback();
          echo "<p>Erro: " . $e->getMessage() . "</p>";
        }
      } else {
        echo "<p>Dados do pedido inválidos.</p>";
      }

      $conn->close();
      ?>
    </div>
  </div>
</body>
</html>
