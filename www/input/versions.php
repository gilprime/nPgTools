<?php
	function DisplayVersionsInLeftMenu()
	{
		/// Ouverture du fichier
		/*$fichier = './versions.xml';
		$fp = fopen($fichier, "r");
		if (!$fp)
		{
			die("Impossible d'ouvrir le fichier XML");		
		}
		fclose($fp);
		
		$xml = simplexml_load_file($fichier);
		$tools = ($xml->tool);	*/
		
		/// Mise en forme
		$result  = "<div id=\"leftpanel\">";

		// Google champ de recherche
		$result .= "<form action=\"http://www.google.com/cse\" id=\"cse-search-box\" target=\"_blank\">";
		$result .= " <div>";
		$result .= "  <input type=\"hidden\" name=\"cx\" value=\"partner-pub-0027145446672449:8kympj-gj80\" />";
		$result .= "  <input type=\"hidden\" name=\"ie\" value=\"UTF-8\" />";
		$result .= "  <input type=\"text\" name=\"q\" size=\"18\" />";
		$result .= "  <input type=\"submit\" name=\"sa\" value=\"Search\" />";
		$result .= " </div>";
		$result .= "</form>";
		$result .= "<script type=\"text/javascript\" src=\"http://www.google.com/cse/brand?form=cse-search-box&amp;lang=en\"></script>";		
		
		$result .= "<div align=\"justify\" class=\"graypanel\">";
		
		/*foreach ($tools as $tool)
		{*/
			$result .= "<span class=\"smalltitle\">nPgDump</span><br />";
			
			/*$versions = ($tool->version);	
			foreach ($versions as $version)
			{*/
				$result .= "<span class=\"smallredtext\"><br />Stable version</span><br />";
				$result .= "<span class=\"bodytext\">&nbsp;&nbsp;&nbsp;&nbsp;Version 1.0 released the 21/10/2010</span><br />";
				$result .= "<a href=\"http://www.google.fr\" class=\"bodytext\">&nbsp;&nbsp;&nbsp;&nbsp;Download now</a><br />";			
			//}
		//}		
		$result .= "</div>";
		$result .= "</div>";
		return $result;
	}
?>