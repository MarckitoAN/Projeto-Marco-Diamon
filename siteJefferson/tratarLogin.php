<?php
session_start();
require 'conexao.php';

if ($_SERVER['REQUEST_METHOD'] == 'POST') {
    $email = $_POST['email'];
    $senha = $_POST['senha'];

    if (empty($email) || empty($senha)) {
        echo "Por favor, preencha todos os campos.";
        exit();
    }

    $stmt = $conn->prepare("SELECT * FROM cliente WHERE email = ?");
    if (!$stmt) {
        die("Erro na preparação da consulta: " . $conn->error);
    }

    $stmt->bind_param("s", $email);
    $stmt->execute();
    $result = $stmt->get_result();

    if ($result->num_rows === 0) {
        echo "Usuário ou senha inválidos.";
    } else {
        $user = $result->fetch_assoc();
        if ($senha === $user['senha']) { 
            $_SESSION['user_id'] = $user['id'];
            header("Location: produtos.php");
            exit();
          } else {
              header("Location: login.php");

        }
    }

    $stmt->close();
}
?>
