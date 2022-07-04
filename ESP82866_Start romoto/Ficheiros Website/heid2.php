<html>
<body>

    <?php
    // Definição das variaveis internas com a informação que vem na url 
   
 /* Conectando, escolhendo o banco de dados */
    $link = mysqli_connect("localhost", "root", "")or die("Não pude conectar: " . mysqli_error());
    mysqli_select_db($link,"alunos") or die("Não pude selecionar o banco de dados");
	/* Fazendo a query SQL DE LEITURA DA BASE DE DADOS*/
    $query = "SELECT * FROM start";
    $result = mysqli_query($link,$query) or die("A query falhou: " . mysqli_error());
    $linha1=mysqli_fetch_array($result,MYSQLI_ASSOC);
    echo " Y1:".$linha1['value1'];
	
	// Definição das variaveis internas com a informação que vem na url 
	
	//$value1=$_POST["Estado_NcStart"];
	
 /* Query SQL de actualizacao */
	//if ($Y0 == "1"){
    $query = "UPDATE start SET value1='" ."none". "' WHERE ID=1";
    mysqli_query($link,$query) or die("A query falhou: " . mysqli_error());	
	//}
/* Liberta o resultado */
/* Fechando a conexão */
    mysqli_close($link);
	
    ?>
</body>
</html>ç