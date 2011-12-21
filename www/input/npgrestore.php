<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<?php
	include 'versions.php';
	include 'footer.php';
	include 'header.php';
	include 'misc.php';
?>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
	<?php echo addCommonHead(); ?>
	<title>nPgRestore</title>
	<!-- syntax_highlighter -->	
	<script type="text/javascript" src="js/syntaxhighlighter/scripts/shCore.js"></script>
	<script type="text/javascript" src="js/syntaxhighlighter/scripts/shBrushCSharp.js"></script>
	<link type="text/css" rel="stylesheet" href="js/syntaxhighlighter/styles/shCoreDefault.css"/>
	<script type="text/javascript">SyntaxHighlighter.all();</script>
</head>
<body>
	<div id="page" align="center">
		<div id="content" style="width:800px">			
			<?php echo DisplayHeader();?>
			
			<div id="contenttext">
				<div style="padding:10px">
					<?php echo addAdSense() ?>
					<span class="titletext">nPgRestore, Restore easily</span>
				</div>
				<div class="bodytext" style="padding:12px;" align="justify">
					<strong>nPgRestore allows you to easily restore your previously dumped database in a .NET/Mono software without deploying all PostgreSQL into your operating system.</strong><br />

					<br />
					nPgRestore is a .Net library that permits you to restore easily your Postgresql database. It allows any program developed for .Net framework to restore an archieved database into the database server. It is written with 100% C# code. It works with all supported versions of Postgresql (see <a href="http://wiki.postgresql.org/wiki/PostgreSQL_Release_Support_Policy" target="_blank"> PostgreSQL wiki</a>).<br /><br />
					Using nPgRestore is easy, the easiest way to make a restoration, is this one :
					<pre class="brush: c-sharp;">
					...
					NpgRestore.NpgRestore.Restore("dbname", "login",
					                              "password", "archiveLocation");
					...
					</pre>
					nPgRestore is very flexible, and a various sets of functions are available, to permit you to choose the right function for your needs.
					<br /><br /><br /><br /><br />

					<table border="0"><tr><td colspan="2" align="center"><strong>nPgRestore - Code quality</strong></td></tr><tr><td><a href="./quality/nPgRestore/Gendarme.html"><img alt="Gendarme - Mono" src="images/gendarme_valid.png" height="64" width="64" border="none"/></a></td><td><strong>nPgRestore respects the Gendarme rules.</strong> Gendarme is a extensible rule-based tool to find problems in .NET applications and libraries. Gendarme inspects programs and libraries that contain code in ECMA CIL format (Mono and .NET) and looks for common problems with the code, problems that compiler do not typically check or have not historically checked.</td></tr><tr><td><a href="./quality/nPgRestore/StyleCop.html"><img alt="StyleCop" src="images/stylecop_valid.png" height="64" width="64" border="none"/></a></td><td><strong>nPgRestore respects the StyleCop rules.</strong> StyleCop analyzes C# source code to enforce a set of style and consistency rules.</td></tr></table>						
				</div>

			</div>
			<?php echo DisplayVersionsInLeftMenu(); ?>
			<?php echo DisplayFooter(); ?>		
		</div>
	</div>
</body>
</html>