imports ["htmlReport", "pdf"] from "reportKit";

#' Config for the runtime environment
#' at package startup.
#' 
const .onLoad = function() {
	options(wkhtmltopdf = "/usr/local/bin/wkhtmltopdf");
}