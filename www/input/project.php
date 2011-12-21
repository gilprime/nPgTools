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
					nPgTools is hosted at pgFoundry.org
					<ul>
					<li><a href="http://pgfoundry.org/">The pgFoundry page</a></li>
					<li><a href="http://pgfoundry.org/projects/npgtools/">The nPgTools project page</a></li>
					<li><a href="http://pgfoundry.org/tracker/?atid=1578&group_id=1000488&func=browse">The nPgTools bugtracker page</a></li>
					<li><a href="http://pgfoundry.org/tracker/?atid=1581&group_id=1000488&func=browse">The nPgTools feature request page</a></li>
					<li><a href="http://pgfoundry.org/forum/?group_id=1000488">The nPgTools forums</a></li>
					<li><a href="http://cvs.pgfoundry.org/cgi-bin/cvsweb.cgi/npgtools/">The nPgTools CVS repository</a></li>
					</ul>
					<br/><br/>
					nPgTools roadmap is in the <a href="roadmap.html">roadmap</a> section
					<br/><br/>
					nPgTools versionning is in the <a href="versioning.html">versioning</a> section
					  
				</div>
			</div>
			<?php echo DisplayVersionsInLeftMenu(); ?>
			<?php echo DisplayFooter(); ?>			
	
		</div>
	</div>
</body>
</html> 
