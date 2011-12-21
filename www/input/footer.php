<?php
	function DisplayFooter()
	{
		/// Mise en forme
		$result  = "<div id=\"footer\" class=\"smallgraytext\">";
		
		$result .= "<a href=\"http://npgtools.projects.postgresql.org/\">nPgTools &copy; 2010</a>";
		$result .= "&nbsp;&nbsp;|&nbsp;&nbsp;";

		$result .= "<a href=\"http://validator.w3.org/check?uri=referer;verbose=1\" target=\"_blank\">Valid XHTML 1.0</a>";
		$result .= "&nbsp;&nbsp;|&nbsp;&nbsp;";

		$result .= "<a href=\"http://jigsaw.w3.org/css-validator/validator?uri=http://npgtools.projects.postgresql.org/images/style.css&profile=css21&usermedium=all&warning=1\" target=\"_blank\">Valid CSS 2.1</a>";
		$result .= "&nbsp;&nbsp;|&nbsp;&nbsp;";

		$result .= "<a href=\"http://www.oswd.org\" target=\"_blank\">Design RedTie from OSWD</a>";
		$result .= "&nbsp;&nbsp;|&nbsp;&nbsp;";

		$result .= "<a href=\"http://gforge.org\" target=\"_blank\">Powered by Gforge</a>";
		
		$result .= "</div>";
		
		$result .= AddAnalyticsCode();
		return $result;
	}
	
	function AddAnalyticsCode()
	{		
		$result = "<script type=\"text/javascript\">
          var _gaq = _gaq || [];
          _gaq.push(['_setAccount', 'UA-15832235-1']);
          _gaq.push(['_trackPageview']);
          (function() {
            var ga = document.createElement('script'); ga.type = 'text/javascript'; ga.async = true;
            ga.src = ('https:' == document.location.protocol ? 'https://ssl' : 'http://www') + '.google-analytics.com/ga.js';
            var s = document.getElementsByTagName('script')[0]; s.parentNode.insertBefore(ga, s);
          })();
        </script>";
        
		return $result;
	}
?>