#addin "wk.StartProcess"

using PS = StartProcess.Processor;
Task("Prepare").Does(() => {
    CopyDirectory("prebuild", "customize.server/PdfAnnotation/wwwroot");
    PS.StartProcess("npm run build");
});

var target = Argument("target", "default");
RunTarget(target);