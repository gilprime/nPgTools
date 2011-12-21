<?php
	function inserSpaces($number = 0)
	{
		$result = "";
		for ($i = 1; $i <= $number; $i++)
		{
		    $result  .= "&nbsp;";
		}		
		return $result;
	}

	function addAdSense()
	{
	  $result  = "<center>";
	  $result .= "<script type=\"text/javascript\"><!--";
	  $result .= "google_ad_client = \"pub-0027145446672449\";";
	  $result .= "/* npgtools - 468x60 */";
	  $result .= "google_ad_slot = \"2474316013\";";
	  $result .= "google_ad_width = 468;";
	  $result .= "google_ad_height = 60;";
	  $result .= "//-->";
	  $result .= "</script>";
	  $result .= "<script type=\"text/javascript\"";
	  $result .= "src=\"http://pagead2.googlesyndication.com/pagead/show_ads.js\">";
	  $result .= "</script>";
	  $result  = "</center>";
	  return $result;
	}
    
    function addCommonHead()
	{
      $result  = "<meta name=\"author\" content=\"nPgTools - Gildas PRIME\" />";
      $result .= "<meta http-equiv=\"Content-Type\" content=\"text/html; charset=iso-8859-1\" />";
      $result .= "<link rel=\"stylesheet\" href=\"style/style.css\" type=\"text/css\" />";
      $result .= "<meta name=\"Date\" content=\"Mon, 03 Jun 2007 03:15:00\">";
      $result .= "<meta http-equiv=\"Expires\" content=\"Mon, 07 Jan 1980 12:11:06\">";


      return $result;
    }
?>