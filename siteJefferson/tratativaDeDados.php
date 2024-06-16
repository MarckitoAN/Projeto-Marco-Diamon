<?php
include_once 'conexao.php';

if ($_SERVER["REQUEST_METHOD"] == "POST") {
	$id_user = 1;
	$nome = $_POST['nome'];
	$rg = $_POST['rg'];
	$cpf = $_POST['cpf'];
	$tel = $_POST['tel'];
	$rua = $_POST['rua'];
	$bairro = $_POST['bairro'];
	$cidade = $_POST['cidade'];
	$estado = $_POST['estado'];
	$email = $_POST['email'];
	$senha = $_POST['senha'];

  

    $stmt = $conn->prepare("INSERT INTO cliente (id_user,nome, rg, cpf, telefone, rua, bairro, cidade, estado, email, senha) VALUES (?,?,?,?,?,?,?,?,?,?,?)");
    $stmt->bind_param("sssssssssss", $id_user,$nome, $rg, $cpf, $tel, $rua, $bairro,$cidade,$estado,$email,$senha);
 
    if ($stmt->execute() === TRUE) {
       header("Location: login.php");
    } else {
      echo "Erro ao cadastrar Cliente: " . $conn->error;
    }
}

