imports ["htmlReport", "pdf"] from "reportKit";

#' Config for the runtime environment
#' at package startup.
#' 
const .onLoad as function() {
	options(wkhtmltopdf = "/usr/local/bin/wkhtmltopdf");
}