<!-- Página que permite a troca de informação entre o arduino e a base de dados -->

<html>
<body>

    <?php
 /* Conecta e escolhe a base de dados*/
 // Neste caso, estamos a connectar ao XAMPP(apache)
 // E a base de dados é "alunos"
    $link = mysqli_connect("localhost", "root", "")or die("Não pude conectar: " . mysqli_error());
    mysqli_select_db($link,"alunos") or die("Não pude selecionar o banco de dados");

//Aqui o php está a acrescentar uma linha nova à tabela "lsv2" da base de dados "alunos" em que:
//na coluna "Emissor" insere "ESP"
//na coluna "Mensagem" insere "Iniciei um programa (NC Start)"
	$query = "INSERT INTO lsv2 (Emissor,Mensagem) VALUES ('ESP','Iniciei Programa(NC Start)');";
	$result = mysqli_query($link,$query) or die("A query falhou: " . mysqli_error());
	
 /* Liberta o resultado */
    mysqli_free_result($result);
 /* Fecha a conexão */
    mysqli_close($link);
 
    ?>
</body>
</html>
