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
	<title>nPgDump</title>
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
					<span class="titletext">nPgDump, Dump easily</span>
				</div>
					
					<div class="bodytext" style="padding:12px;" align="justify">
					<strong>nPgDump allows you to easily dump your database in a .NET/Mono software without deploying all PostgreSQL into your operating system.</strong><br />

					<br />
					nPgDump is a .Net library that permits you to dump easily your Postgresql database. It allows any program developed for .Net framework to make a dump of the database server. It is written with 100% C# code. It works with all supported versions of Postgresql (see <a href="http://wiki.postgresql.org/wiki/PostgreSQL_Release_Support_Policy" target="_blank"> PostgreSQL wiki</a>).<br /><br />
					Using nPgDump is easy, the easiest way to make a dump, is this one :
					<pre class="brush: c-sharp;">
					...
					NpgDump.NpgDump.Dump("dbname", "login", "password");
					...
					</pre>
					nPgDump is very flexible, and a various sets of functions are available, to permit you to choose the right function for your needs.
					<br /><br /><br /><br /><br />

					<table border="0"><tr><td colspan="2" align="center"><strong>nPgDump - Code quality</strong></td></tr><tr><td><a href="./quality/npgtools/gendarme.html"><img alt="Gendarme - Mono" src="images/gendarme_valid.png" height="64" width="64" border="none"/></a></td><td><strong>nPgDump respects the Gendarme rules.</strong> Gendarme is a extensible rule-based tool to find problems in .NET applications and libraries. Gendarme inspects programs and libraries that contain code in ECMA CIL format (Mono and .NET) and looks for common problems with the code, problems that compiler do not typically check or have not historically checked.</td></tr><tr><td><a href="./quality/npgtools/stylecop.html"><img alt="StyleCop" src="images/stylecop_valid.png" height="64" width="64" border="none"/></a></td><td><strong>nPgDump respects the StyleCop rules.</strong> StyleCop analyzes C# source code to enforce a set of style and consistency rules.</td></tr></table>						
				</div>			
			</div>
			<?php echo DisplayVersionsInLeftMenu(); ?>
			<?php echo DisplayFooter(); ?>		
		</div>
	</div>
</body>
</html>