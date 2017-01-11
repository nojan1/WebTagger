using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebTagger.Tests
{
    class StaticWebPageContent
    {
        public static readonly string Html = @"
<!doctype html>
<!-- Website Template by freewebsitetemplates.com -->
<html>
<head>
	<base href=""https://f271.https.cdn.softlayer.net/80F271/dal05.objectstorage.softlayer.net/v1/AUTH_b4a3a2ca-226e-4f3c-aef2-b5275f5900cc/cdn/preview/mustacheenthusiast/"" />
	<meta charset=""UTF-8"">
	<meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
	<title>Mustache Enthusiast</title>
	<link rel=""stylesheet"" type=""text/css"" href=""css/style.css"">
	<link rel=""stylesheet"" type=""text/css"" href=""css/mobile.css"" media=""screen and (max-width : 568px)"">
	
	<script type=""text/javascript"" src=""js/mobile.js""></script>
	<link rel=""stylesheet"" type=""text/css"" href=""https://freewebsitetemplates.com/preview/shared/previews.css"" />

	
	<!-- Google Analytics -->
	<script>
	(function(i,s,o,g,r,a,m){i['GoogleAnalyticsObject']=r;i[r]=i[r]||function(){
	(i[r].q=i[r].q||[]).push(arguments)},i[r].l=1*new Date();a=s.createElement(o),
	m=s.getElementsByTagName(o)[0];a.async=1;a.src=g;m.parentNode.insertBefore(a,m)
	})(window,document,'script','//www.google-analytics.com/analytics.js','ga');
	
	ga('create', 'UA-241068-1', 'auto');
	ga('send', 'pageview');
	
	</script>
	<!-- End Google Analytics -->
	

	<script src=""https://freewebsitetemplates.com/js/jquery/jquery-1.11.0.min.js""></script>	
		
	<script src=""https://freewebsitetemplates.com/js/xenforo/xenforo.js?_v=77c96446""></script>


	<script>
	$(document).ready(function() {
		$(""a[data-ga-event='click']"").each(
			function() {
				if ($(this).data(""gaCategory"") && $(this).data(""gaAction"") && $(this).data(""gaLabel"")) {
					//console.log($(this).data(""gaCategory"") + "" - "" + $(this).data(""gaAction"") + "" - "" + $(this).data(""gaLabel""));
					$(this).on('click', function() {
						ga('send', 'event', $(this).data(""gaCategory""), $(this).data(""gaAction""), $(this).data(""gaLabel""));
						Piwik.getAsyncTracker().trackEvent($(this).data(""gaCategory""), $(this).data(""gaAction""), $(this).data(""gaLabel""));
					});
				} else {
					console.error(""missing attributes for google analytics event tracking"");
				}
			}
		);
	});
	</script>

</head>
<body> <!-- Start Fixed Template Info Header -->
	<div id=""templateInfo"">
		<div>
			<h2><a target=""_blank"" href=""https://freewebsitetemplates.com/"">&nbsp;<!-- FREE WEBSITE TEMPLATES --></a></h2>
				<ul class=""navigation"">
					
					<li class=""download""><a href=""https://freewebsitetemplates.com/download/mustacheenthusiast/"" data-ga-event=""click"" data-ga-category=""Previews Top Bar"" data-ga-action=""Download Click"" data-ga-label=""Mustache Enthusiast"">Download</a></li>     
					<li class=""discuss""><a href=""https://freewebsitetemplates.com/discuss/mustacheenthusiast/"" data-ga-event=""click"" data-ga-category=""Previews Top Bar"" data-ga-action=""Discuss Click"" data-ga-label=""Mustache Enthusiast"">Discuss</a></li>     		     
				</ul>
		</div>
	</div>  
	<div id=""clearance"">&nbsp;</div>
	<!-- End  Fixed Template Info Header -->
	
	
	
	<div id=""header"">
		<a href=""https://freewebsitetemplates.com/preview/mustacheenthusiast/index.html"" class=""logo"">
			<img src=""images/logo.jpg"" alt="""">
		</a>
		<ul id=""navigation"">
			<li class=""selected"">
				<a href=""https://freewebsitetemplates.com/preview/mustacheenthusiast/index.html"">home</a>
			</li>
			<li>
				<a href=""https://freewebsitetemplates.com/preview/mustacheenthusiast/about.html"">about</a>
			</li>
			<li>
				<a href=""https://freewebsitetemplates.com/preview/mustacheenthusiast/gallery.html"">gallery</a>
			</li>
			<li>
				<a href=""https://freewebsitetemplates.com/preview/mustacheenthusiast/blog.html"">blog</a>
			</li>
			<li>
				<a href=""https://freewebsitetemplates.com/preview/mustacheenthusiast/contact.html"">contact</a>
			</li>
		</ul>
	</div>
	<div id=""body"">
		<div id=""featured"">
			<img src=""images/the-beacon.jpg"" alt="""">
			<div>
				<h2>the beacon to all mankind</h2>
				<span>Our website templates are created with</span>
				<span>inspiration, checked for quality and originality</span>
				<span>and meticulously sliced and coded.</span>
				<a href=""https://freewebsitetemplates.com/preview/mustacheenthusiast/blog-single-post.html"" class=""more"">read more</a>
			</div>
		</div>
		<ul>
			<li>
				<a href=""https://freewebsitetemplates.com/preview/mustacheenthusiast/gallery.html"">
					<img src=""images/the-father.jpg"" alt="""">
					<span>the father</span>
				</a>
			</li>
			<li>
				<a href=""https://freewebsitetemplates.com/preview/mustacheenthusiast/gallery.html"">
					<img src=""images/the-actor.jpg"" alt="""">
					<span>the actor</span>
				</a>
			</li>
			<li>
				<a href=""https://freewebsitetemplates.com/preview/mustacheenthusiast/gallery.html"">
					<img src=""images/the-nerd.jpg"" alt="""">
					<span>the nerd</span>
				</a>
			</li>
		</ul>
	</div>
	<div id=""footer"">
		<div>
			<p>&copy; 2023 by Mustacchio. All rights reserved.</p>
			<ul>
				<li>
					<a href=""http://freewebsitetemplates.com/go/twitter/"" id=""twitter"">twitter</a>
				</li>
				<li>
					<a href=""http://freewebsitetemplates.com/go/facebook/"" id=""facebook"">facebook</a>
				</li>
				<li>
					<a href=""http://freewebsitetemplates.com/go/googleplus/"" id=""googleplus"">googleplus</a>
				</li>
				<li>
					<a href=""http://pinterest.com/fwtemplates/"" id=""pinterest"">pinterest</a>
				</li>
			</ul>
		</div>
	</div>
	<div id=""wix"" style=""padding:20px 0;text-align:center;"">
		
	</div>
	</div><!-- end of templatePreview--><script>


jQuery.extend(true, XenForo,
{
	visitor: { user_id: 0 },
	serverTimeInfo:
	{
		now: 1484134981,
		today: 1484114400,
		todayDow: 3
	},
	_lightBoxUniversal: ""0"",
	_enableOverlays: ""1"",
	_animationSpeedMultiplier: ""1"",
	_overlayConfig:
	{
		top: ""10%"",
		speed: 200,
		closeSpeed: 100,
		mask:
		{
			color: ""rgb(255, 255, 255)"",
			opacity: ""0.6"",
			loadSpeed: 200,
			closeSpeed: 100
		}
	},
	_ignoredUsers: [],
	_loadedScripts: [],
	_cookieConfig: { path: ""/"", domain: """", prefix: ""xf_""},
	_csrfToken: """",
	_csrfRefreshUrl: ""login/csrf-token-refresh"",
	_jsVersion: ""77c96446"",
	_noRtnProtect: false,
	_noSocialLogin: false
});
jQuery.extend(XenForo.phrases,
{
	cancel: ""Cancel"",

	a_moment_ago:    ""A moment ago"",
	one_minute_ago:  ""1 minute ago"",
	x_minutes_ago:   ""%minutes% minutes ago"",
	today_at_x:      ""Today at %time%"",
	yesterday_at_x:  ""Yesterday at %time%"",
	day_x_at_time_y: ""%day% at %time%"",

	day0: ""Sunday"",
	day1: ""Monday"",
	day2: ""Tuesday"",
	day3: ""Wednesday"",
	day4: ""Thursday"",
	day5: ""Friday"",
	day6: ""Saturday"",

	_months: ""January,February,March,April,May,June,July,August,September,October,November,December"",
	_daysShort: ""Sun,Mon,Tue,Wed,Thu,Fri,Sat"",

	following_error_occurred: ""The following error occurred"",
	server_did_not_respond_in_time_try_again: ""The server did not respond in time. Please try again."",
	logging_in: ""Logging in"",
	click_image_show_full_size_version: ""Click this image to show the full-size version."",
	show_hidden_content_by_x: ""Show hidden content by {names}""
});

// Facebook Javascript SDK
XenForo.Facebook.appId = """";
XenForo.Facebook.forceInit = false;


</script>
<!-- Piwik -->
<script type=""text/javascript"">
  var _paq = _paq || [];
  _paq.push(['trackPageView']);
  _paq.push(['enableLinkTracking']);
  (function() {
    var u=""//piwik.freewebsitetemplates.com/"";
    _paq.push(['setTrackerUrl', u+'piwik.php']);
    _paq.push(['setSiteId', 1]);
    var d=document, g=d.createElement('script'), s=d.getElementsByTagName('script')[0];
    g.type='text/javascript'; g.async=true; g.defer=true; g.src=u+'piwik.js'; s.parentNode.insertBefore(g,s);
  })();
</script>
<noscript><p><img src=""https://freewebsitetemplates.com//piwik.freewebsitetemplates.com/piwik.php?idsite=1"" style=""border:0;"" alt="""" /></p></noscript>
<!-- End Piwik Code -->

</body>
</html>
";
    }
}
