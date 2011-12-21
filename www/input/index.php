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
					<span class="titletext">nPgTools, Tools for PostgreSQL</span>
					</div>
					<div class="bodytext" style="padding:12px;" align="justify">
					<strong>nPgTools is a set of tools designed to perform operations on a PostgreSQL database, directly from the C# code.</strong>
					<br /><br />
					Need to perform a <strong>pg_dump</strong> or a <strong>pg_restore</strong> from a remote computer
					that don't have a PostgreSQL installed on it ? Using nPgTools permits you to do that quickly
					without deploying PostgreSQL or the pg_dump/pg_restore and dependant related files. See
					the <a href="npgdump.html">nPgDump</a> and <a href="npgdump.html">nPgRestore</a> class from
					nPgTools.
					<br/>
					<center>_______________________________________</center>
					<br/>
					<!-- NEWS -->
					<strong><u>24/12/2010 :</u>&nbsp;&nbsp;nPgTools is out</strong><br/>
					<div id="newscontenttext">
					  The details of the roadmap is in the Roadmap section, the names of the branches will follow the 
					  elephant species <i>(all known extant and extinct species)</i>. See
					  <a href="http://en.wikipedia.org/wiki/List_of_elephant_species" target="_blank">Wikipedia</a> for the list of the elephant species.<br/>
					  The versionning of the project is detailed in the project section, see <a href="verionning.html">this page</a>
					  for more details.</br>
					  </br></br>
					</div>
	
				</div>
			</div>
			<?php echo DisplayVersionsInLeftMenu(); ?>
			<?php echo DisplayFooter(); ?>			
	
		</div>
	</div>
</body>
</html>