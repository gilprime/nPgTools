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
					<span class="titletext">nPgTools versioning</span>
				</div>
				<div class="bodytext" style="padding:12px;" align="justify">
					nPgTools uses a four-sequence identifier for versioning, each release is assigned to a unique version.
					<ul>
					<li>The first sequence is incremented on major modification <i>(when the code is completely rewritten)</i>, or when a 
					new module has been added to nPgTools;</li>
					<li>The second sequence is incremented on minor modification, like a new feature added to an existant module, and return to 0
					at each major modification;</li>
					<li>The third sequence is incermented on debug, he his incremented at each build, and return to 0 at each minor modification;</li>
					<li>The fourth sequence represents the version of the PostgreSQL libraries used for the build <i>(for example, 901 for the 9.0.1 build).</i></li>
					</ul>
					
				</div>
			</div>
			<?php echo DisplayVersionsInLeftMenu(); ?>
			<?php echo DisplayFooter(); ?>			
	
		</div>
	</div>
</body>
</html> 
 
 
