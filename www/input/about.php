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
	<title>About nPgTools</title>
</head>
<body>
	<div id="page" align="center">
		<div id="content" style="width:800px">
			<?php echo DisplayHeader();?>
			<div id="contenttext">
				<div style="padding:10px">
					<?php echo addAdSense() ?>
					<span class="titletext">About nPgTools</span>
				</div>						
				<div class="bodytext" style="padding:12px;" align="justify">
					nPgTools uses several tools and ressorces, here are the most importants :<br />
					<br />

					<!-- nPgTools - WebSite -->
					<table border="0">
						<tr>
							<td colspan="2" align="center"><strong>nPgTools - WebSite</strong></td>
						</tr>
						<tr>
							<td><a href="http://www.oswd.org" target="_blank"><img alt="Open Source Web Design" src="images/oswd_logo.png" width="88" border="none"/></a></td>
							<td>Open Source Web Design goal is to provide the Open Source community quality web designs to help get people's projects on the web in a way that is both organized and good looking.</td>

						</tr>
						<tr>
							<td><a href="http://www.crystalxp.net" target="_blank"><img alt="Crystal XP" src="images/crystalxp_logo.png" width="88" border="none"/></a></td>
							<td>CrystalXP offers a large gallery of Wallpapers, Iconsets, 3D animations, and BricoPacks !
							The <a href="http://www.crystalxp.net/galerie/fr.id.132-refresh-cl-tpdk-icones-a-png.htm" target="_blank">Refresh CL</a> pack is used in this website.</td>
						</tr>
						<tr>
							<td><a href="http://www.gimp.org" target="_blank"><img alt="The GIMP" src="images/gimp_logo.png" width="88" border="none"/></a></td>

							<td>GIMP is the GNU Image Manipulation Program. It is a freely distributed piece of software for such tasks as photo retouching, image composition and image authoring. It works on many operating systems, in many languages.</td>
						</tr>
						<tr>
							<td><a href="http://notepad-plus.sourceforge.net/uk/site.htm" target="_blank"><img alt="Notepad++" src="images/notepadplusplus_logo.png" width="88" border="none"/></a></td>
							<td>Notepad++ is a free source code editor and Notepad replacement that supports several languages. Running in the MS Windows environment, its use is governed by GPL License.</td>
						</tr>
						<tr>
							<td><a href="http://alexgorbatchev.com/wiki/SyntaxHighlighter" target="_blank"><img alt="SyntaxHighlighter" src="images/syntaxhighlighter_logo.png" width="88" border="none"/></a></td>

							<td>SyntaxHighlighter is a fully functional self-contained code syntax highlighter developed in JavaScript. To get an idea of what SyntaxHighlighter is capable of, have a look at the <a href="http://alexgorbatchev.com/wiki/SyntaxHighlighter:Demo" target="_blank">demo page.</a></td>
						</tr>
						
					</table>
					
					<br /><br />
					
					<!-- nPgTools - Developpement -->
					<table border="0">
						<tr><td colspan="2" align="center"><strong>nPgTools - Developpement tools</strong></td>

						</tr>
						<tr>
							<td><a href="http://pgfoundry.org/" target="_blank"><img alt="PgFoundry" src="images/pgfoundry.png" width="88" border="none"/></a></td>
							<td>PgFoundry is the PostgreSQL Development Group's site for developing and publishing PostgreSQL-related software that is not part of the core product. It runs on GForge, the Open Source collaborative software development tool.</td>
						</tr>
						<tr>
							<td><a href="http://www.icsharpcode.net/opensource/sd/" target="_blank"><img alt="SharpDevelop" src="images/sharpdevelop_logo.png" width="88" border="none"/></a></td>
							<td>SharpDevelop is a free IDE for C#, VB.NET and Boo projects  on Microsoft's .NET platform. It is open-source, and you can download both sourcecode and executables from <a href="http://www.icsharpcode.net/opensource/sd/" target="_blank">this site</a>.
							</td>

						</tr>
					</table>
					
					<br /><br />
					<table border="0"><tr><td colspan="2" align="center"><strong>nPgTools - Code quality</strong></td></tr><tr><td><a href="http://www.mono-project.com/Gendarme" target="_blank"><img alt="Gendarme - Mono" src="images/gendarme_valid.png" height="64" width="64" border="none"/></a></td><td><strong>nPgTools respects the Gendarme rules.</strong> Gendarme is a extensible rule-based tool to find problems in .NET applications and libraries. Gendarme inspects programs and libraries that contain code in ECMA CIL format (Mono and .NET) and looks for common problems with the code, problems that compiler do not typically check or have not historically checked.</td></tr><tr><td><a href="http://code.msdn.microsoft.com/sourceanalysis" target="_blank"><img alt="StyleCop" src="images/stylecop_valid.png" height="64" width="64" border="none"/></a></td><td><strong>nPgTools respects the StyleCop rules.</strong> StyleCop analyzes C# source code to enforce a set of style and consistency rules.</td></tr></table>						
				</div>

			</div>
			<?php echo DisplayVersionsInLeftMenu(); ?>
			<?php echo DisplayFooter(); ?>					
		</div>
	</div>
</body>
</html>