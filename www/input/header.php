<?php
		// -------------------------------
		//	<div id=\"logo\">
		//		<div style=\"margin-top:70px\" class=\"whitetitle\"></div>
		//	</div>
		//	<div id=\"topheader\">
		//		<div align=\"left\" class=\"bodytext\">
		//			<br />
		//			<strong>.Net tools for PostgreSQL</strong><br />
		//			to make easier your<br />
		//			developpements
		//		</div>
		//		<div id=\"toplinks\" class=\"smallgraytext\">
		//			<a href=\"#\">Home</a> | <a href=\"#\">Sitemap</a> | <a href=\"#\">Contact Us</a>
		//		</div>
		//	</div>
		//	<div id=\"menu\">
		//		<div align=\"right\" class=\"smallwhitetext\" style=\"padding:9px;\">
		//			<a href=\"#\">Home</a> | <a href=\"#\">Products</a> | <a href=\"#\">Forums</a> | <a href=\"#\">Contact Us</a>
		//		</div>
		//	</div>
		//	<div id=\"submenu\">
		//		<div align=\"right\" class=\"smallgraytext\" style=\"padding:9px;\">
		//			<a href=\"#\">nPgDump</a> | <a href=\"#\">nPgRestore</a> | <a href=\"#\">nPgBench</a> | <a href=\"#\">nPgConf</a> | <a href=\"#\">npgsql builds</a>
		//		</div>
		//	</div>
		// -------------------------------

	function DisplayHeader()
	{
		// Get current file name
		$currentFile = $_SERVER["SCRIPT_NAME"];
		$parts = Explode('/', $currentFile);
		$currentFile = $parts[count($parts) - 1];
					
		// Create the header
		//	<div id=\"logo\">
		//		<div style=\"margin-top:70px\" class=\"whitetitle\"></div>
		//	</div>
		
		$result  = "<div id=\"logo\">";
		$result .= "<div style=\"margin-top:70px\" class=\"whitetitle\"></div>";
		$result .= "</div>";
		
		//	<div id=\"topheader\">
		//		<div align=\"left\" class=\"bodytext\">
		//			<br />
		//			<strong>.Net tools for PostgreSQL</strong><br />
		//			to make easier your<br />
		//			developpements
		//		</div>
		//		<div id=\"toplinks\" class=\"smallgraytext\">
		//			<a href=\"#\">Home</a> | <a href=\"#\">Sitemap</a> | <a href=\"#\">Contact Us</a>
		//		</div>
		//	</div>
		$result .= "<div id=\"topheader\">";
		$result .= "<div align=\"left\" class=\"bodytext\">";
		$result .= "<br />";
		$result .= "<strong>.Net tools for PostgreSQL</strong><br />";
		$result .= "&nbsp;&nbsp;&nbsp;<i>Because free rhymes with quality</i><br />";
		$result .= "</div>";
		$result .= "<div id=\"toplinks\" class=\"smallgraytext\">";
		$result .= "<a href=\"http://npgtools.projects.postgresql.org/\">Home</a>";
		$result .= " | ";
		$result .= "<a href=\"sitemap.html\">Sitemap</a>";
		$result .= " | ";
		$result .= "<a href=\"#\">Contact Us</a>";
		$result .= "</div>";
		$result .= "</div>";
		
		//	<div id=\"menu\">
		//		<div align=\"right\" class=\"smallwhitetext\" style=\"padding:9px;\">
		//			<a href=\"#\">Home</a> | <a href=\"#\">Products</a> | <a href=\"#\">Forums</a> | <a href=\"#\">Contact Us</a>
		//		</div>
		//	</div>
		$result .= "<div id=\"menu\">";
		$result .= "<div align=\"right\" class=\"smallwhitetext\" style=\"padding:9px;\">";
		$result .= "<img src=\"../images/menu_home.png\" width=\"16\" height=\"16\"/>&nbsp;";
		$result .= "<a href=\"http://npgtools.projects.postgresql.org/\">Home</a>";
		$result .= " | ";
		$result .= "<img src=\"../images/menu_project.png\" width=\"16\" height=\"16\"/>&nbsp;";
		$result .= "<a href=\"project.html\">Project</a>";
		$result .= " | ";
		$result .= "<img src=\"../images/menu_modules.png\" width=\"16\" height=\"16\"/>&nbsp;";
		$result .= "<a href=\"products.html\">Modules</a>";
		$result .= " | ";
		$result .= "<img src=\"../images/menu_forums.png\" width=\"16\" height=\"16\"/>&nbsp;";
		$result .= "<a href=\"http://pgfoundry.org/forum/?group_id=1000488\" target=\"_blank\">Forums</a>";
		$result .= " | ";
		$result .= "<img src=\"../images/menu_bug.png\" width=\"16\" height=\"16\"/>&nbsp;";
		$result .= "<a href=\"http://pgfoundry.org/tracker/?group_id=1000488\">Report a bug</a>";
		$result .= " | ";
		$result .= "<img src=\"../images/menu_about.png\" width=\"16\" height=\"16\"/>&nbsp;";
		$result .= "<a href=\"about.html\">About</a>";
		$result .= "</div>";
		$result .= "</div>";
	
		//	<div id=\"submenu\">
		//		<div align=\"right\" class=\"smallgraytext\" style=\"padding:9px;\">
		//			<a href=\"#\">nPgDump</a> | <a href=\"#\">nPgRestore</a> | <a href=\"#\">nPgBench</a> | <a href=\"#\">nPgConf</a> | <a href=\"#\">npgsql builds</a>
		//		</div>
		//	</div>
		
		$result .= "<div id=\"submenu\">";
		$result .= "<div align=\"right\" class=\"smallgraytext\" style=\"padding:9px;\">";
		
		switch ($currentFile)
		{
			case "npgdump.php":
				$result .= "<a href=\"npgdump.html\"><font color='#FF9933'>nPgDump</font></a>";
				$result .= " | ";
				$result .= "<a href=\"npgrestore.html\">nPgRestore</a>";
				break;
			case "npgrestore.php":
				$result .= "<a href=\"npgdump.html\">nPgDump</a>";
				$result .= " | ";
				$result .= "<a href=\"npgrestore.html\"><font color='#FF9933'>nPgRestore</font></a>";
				break;
			case "products.php":
				$result .= "<a href=\"npgdump.html\">nPgDump</a>";
				$result .= " | ";
				$result .= "<a href=\"npgrestore.html\">nPgRestore</a>";
				break;	
			case "project.php":
				$result .= "<a href=\"roadmap.html\">Roadmap</a>";
				$result .= " | ";
				$result .= "<a href=\"versioning.html\">Versioning</a>";
				break;
			case "roadmap.php":
				$result .= "<a href=\"roadmap.html\"><font color='#FF9933'>Roadmap</font></a>";
				$result .= " | ";
				$result .= "<a href=\"versioning.html\">Versioning</a>";
				break;
			case "versioning.php":
				$result .= "<a href=\"roadmap.html\">Roadmap</a>";
				$result .= " | ";
				$result .= "<a href=\"versioning.html\"><font color='#FF9933'>Versioning</font></a>";
				break;			
		}
		$result .= "</div>";
		$result .= "</div>";		
		
		return $result;
	}
?>