<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN"
"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<?php
	include 'versions.php';
	include 'footer.php';
	include 'header.php';
	include 'misc.php';
?>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
	<?php echo addCommonHead(); ?>
	<title>nPgTools, dump and restore in .Net</title>
</head>
<body>
	<div id="page" align="center">
		<div id="content" style="width:800px">
			<?php echo DisplayHeader();?>
			<div id="contenttext">
				<div style="padding:10px">
					<?php echo addAdSense() ?>
					<span class="titletext">npgTools project details</span>
					</div>
					<div class="bodytext" style="padding:12px;" align="justify">
					the names of the branches will follow the elephant species <i>(all known extant and extinct species)</i>. See
					<a href="http://en.wikipedia.org/wiki/List_of_elephant_species">Wikipedia</a> for the list of the elephant species.
					A little wink to the PostgreSQL mascot.<br/>
					<br/><br/><br/>
					The first official release will be <strong>adaurora</strong> <i>(<a href="http://en.wikipedia.org/wiki/Loxodonta_adaurora">Loxodonta africana adaurora</a>)</i> and will contain
					<ul>
					<li>nPgDump : Dump from a given NpgsqlConnection</li>
					<li>nPgDump : Dump from given IP, Port, database name</li>
					<li>nPgRestore : Restore to a given NpgsqlConnection</li>
					<li>nPgRestore : Restore to given IP, Port, database name and file</li>
					</ul>
					
					The next release will be <strong>borneensis</strong> <i>(<a href=http://en.wikipedia.org/wiki/Borneo_Elephant">Elephas maximus borneensis</a>)</i> and will contain
					<ul>
					<li>nPgDump : Management of all the different options via Flags</li>
					<li>nPgRestore : Management of all the different options via Flags</li>
					</ul>
					
					Later...
					<ul>
					<li>nPgConf : Management of the postgresql.conf and pg_hba.conf files <i>(read, write, modifications, revert modifications...)</i></li>
					<li>nPgBench : Management of pg_bench</li>
					<li>Creation of a utility to find the best conf settings, based on the nPgBench and npgConf modules</li>
					<li>Mono/Linux compatibility</li>
					</ul>					  
				</div>
			</div>
			<?php echo DisplayVersionsInLeftMenu(); ?>
			<?php echo DisplayFooter(); ?>			
	
		</div>
	</div>
</body>
</html> 
 
