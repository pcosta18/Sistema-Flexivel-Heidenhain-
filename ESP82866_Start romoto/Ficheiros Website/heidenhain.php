<html>
<html lang="pt">
  <head>
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <meta name="description" content="">
    <meta name="author" content="Mark Otto, Jacob Thornton, and Bootstrap contributors">
    <meta name="generator" content="Hugo 0.98.0">
    <title>HEIDENHAIN</title>

    <link rel="canonical" href="https://getbootstrap.com/docs/5.2/examples/dashboard/">

    
<link href="../assets/dist/css/bootstrap.min.css" rel="stylesheet">

    <style>
      .bd-placeholder-img {
        font-size: 1.125rem;
        text-anchor: middle;
        -webkit-user-select: none;
        -moz-user-select: none;
        user-select: none;
      }

      @media (min-width: 768px) {
        .bd-placeholder-img-lg {
          font-size: 3.5rem;
        }
      }

      .b-example-divider {
        height: 3rem;
        background-color: rgba(0, 0, 0, .1);
        border: solid rgba(0, 0, 0, .15);
        border-width: 1px 0;
        box-shadow: inset 0 .5em 1.5em rgba(0, 0, 0, .1), inset 0 .125em .5em rgba(0, 0, 0, .15);
      }

      .b-example-vr {
        flex-shrink: 0;
        width: 1.5rem;
        height: 100vh;
      }

      .bi {
        vertical-align: -.125em;
        fill: currentColor;
      }

      .nav-scroller {
        position: relative;
        z-index: 2;
        height: 2.75rem;
        overflow-y: hidden;
      }

      .nav-scroller .nav {
        display: flex;
        flex-wrap: nowrap;
        padding-bottom: 1rem;
        margin-top: -1px;
        overflow-x: auto;
        text-align: center;
        white-space: nowrap;
        -webkit-overflow-scrolling: touch;
      }
	  .center {
		display: block;
		margin-left: auto;
		margin-right: auto;
		width: 60%;
	  }
    </style>

    
    <!-- Custom styles for this template -->
    <link href="dashboard.css" rel="stylesheet">
  </head>
  
  <body>
    
<header class="navbar navbar-dark sticky-top bg-dark flex-md-nowrap p-0 shadow">
  <a class="navbar-brand col-md-3 col-lg-2 me-0 px-3 fs-6" href="#">Centro Maquinagem Heidenhain</a>
  <button class="navbar-toggler position-absolute d-md-none collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#sidebarMenu" aria-controls="sidebarMenu" aria-expanded="false" aria-label="Toggle navigation">
    <span class="navbar-toggler-icon"></span>
  </button>
  <!--<input class="form-control form-control-dark w-100 rounded-0 border-0" type="text" placeholder="Pesquisa" aria-label="Search">-->
  <div class="navbar-nav">
    <div class="nav-item text-nowrap">
      <a class="nav-link px-3" href="Ajuda.html">Ajuda</a>
    </div>
  </div>
</header>



</body>
<div class="container-fluid">
  <div class="row">
    <nav id="sidebarMenu" class="col-md-3 col-lg-2 d-md-block bg-light sidebar collapse">
      <div class="position-sticky pt-3">
        <ul class="nav flex-column">
		  <li class="nav-item">
            <a class="nav-link" >
              <span align="justify" data-feather="shopping-cart" class="align-text-bottom"></span>
              <p align="justify"> Esta página destina-se ao controlo e supervisão do centro de maquinagem HEIDENHAIN TNC 426 PB 
			  presente no laboratório de Sistemas Flexíveis de Produção do departamento de Engenharia Mecânica </p>
            </a>
          </li>

        <h6 class="sidebar-heading d-flex justify-content-between align-items-center px-3 mt-4 mb-1 text-muted text-uppercase">
          <span>Links Úteis</span>
          <a class="link-secondary" href="#" aria-label="Add a new report">
            <span data-feather="plus-circle" class="align-text-bottom"></span>
          </a>
        </h6>
        <ul class="nav flex-column mb-2">
		  <li class="nav-item">
            <a class="nav-link" href="https://www.ua.pt/pt/dem/">
              Sistemas Flaxíveis de Produção
            </a>
          </li>
		  <li class="nav-item">
            <a class="nav-link" href="https://www.ua.pt/pt/uc/15272">
              Projeto em Sistemas de Automação
            </a>
          </li>
		  <li class="nav-item">
            <a class="nav-link" href="https://www.ua.pt/pt/dem/">
              Departamento de Engenharia Mecânica
            </a>
          </li>
		  <li class="nav-item">
            <a class="nav-link" href="https://www.ua.pt/">
              <span data-feather="file-text" class="align-text-bottom"></span>
              Universidade de Aveiro
            </a>
          </li>
		  <ul class="nav flex-column mb-2">
          <li class="nav-item">
            <img src="UA.jpg" width="250" height="140">
          </li>
        </ul>
      </div>
    </nav>
	
	<main class="col-md-9 ms-sm-auto col-lg-10 px-md-4">
      
	  	<div class="row">
		
			<div class="col" >
			<div class="d-flex justify-content-between flex-wrap flex-md-nowrap align-items-center pt-3 pb-2 mb-3 border-bottom">
        <!--<h1 class="h2">Projeto em Automação</h1>-->
	  </div>
	  
				<img src="heid_grande.jpg" width="600" height="300" class="center">
				
				<p align="justify">
			O centro de Maquinagem consiste numa fresadora vertical de 3 eixos atuados por servomotores, com um curso útil de 160x200x130mm, possuindo troca automática de ferramenta com armazém rotativo para 6 ferramentas, fixação automática de peças e simulação gráfica de maquinagem. 
			É controlado por um comando numérico Heidenhain TNC 426 PB, com comunicação RS232, RS422 e ethernet, e suporta os protocolos FE e LSV2.
			</p>
			</div>
		<div class="col">
			<div class="d-flex justify-content-between flex-wrap flex-md-nowrap align-items-center pt-3 pb-2 mb-3 border-bottom">
        <h1 class="h2">Supervisão e Controlo</h1>
	  </div>
		<form action="heidenhain.php" method="POST"> 
		<input type="hidden" name="saidaY1" value="start_remoto">
		<input type="submit" value="Start Remoto"> 
		</form>
		<?php 
		$value1=$_POST['saidaY1'];		
		
		/* Conectando, escolhendo o banco de dados */ 
		$link = mysqli_connect("localhost", "root", "")or die("Nao pude conectar: " . mysqli_error()); 
		mysqli_select_db($link,"alunos") or die("Nao pude selecionar o banco de dados"); 
		/* Query SQL de actualizacao */ 
		$query = "UPDATE start SET value1='" . $value1 . "' WHERE id=1"; 
		$result = mysqli_query($link,$query) or die("A query falhou: " . mysqli_error()); 
		//echo "------- Supervisao -------<br>Os valores lidos na base de dados sao:"; 
		/* Fazendo a query SQL DE LEITURA DA BASE DE DADOS*/ 
		$query = "SELECT * FROM start"; 
		$result = mysqli_query($link,$query) or die("A query falhou: " . mysqli_error()); 
		$linha1=mysqli_fetch_array($result,MYSQLI_ASSOC);
		echo "------- Supervisao -------<br>Os valores lidos na base de dados sao:";
		//If ($linha1['value1'] == "start_remoto")
		//{
			//mysqli_query($con,"UPDATE start SET checkedin=0 WHERE tagid='$arduinobericht' ");
			echo $linha1['value1'];
		
		//} 
		//echo " Estado:". $value1 . "' WHERE id=1";
		/* Liberta o resultado */ 
		mysqli_free_result($result); 
		/* Fechando a conex„o */ 
		mysqli_close($link); 
		//}
		?>
		</div>
		</div>
	  
	  
      <div class="d-flex justify-content-between flex-wrap flex-md-nowrap align-items-center pt-3 pb-2 mb-3 border-bottom">
        <h1 class="h2">Supervisão Grafana</h1>
	  </div>
	  <iframe src="http://localhost:3000/d/lNbfOQlnk/new-dashboard?orgId=1&from=1656924995516&to=1656946595516&viewPanel=2" 
	  width="450" 
	  height="200" 
	  frameborder="0">
	  </iframe>
	  
		<!--<meta http-equiv="refresh" content="2">-->
</body>
	  
	  