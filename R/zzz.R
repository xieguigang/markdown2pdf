imports ["html", "pdf"] from "reportKit";

#' Config for the runtime environment
#' at package startup.
#' 
const zzz as function() {
	options(wkhtmltopdf = "/usr/local/bin/wkhtmltopdf");
}