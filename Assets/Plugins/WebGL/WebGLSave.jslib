mergeInto(LibraryManager.library, {
    DownloadFile: function (filenamePtr, base64Ptr) {
        var filename = UTF8ToString(filenamePtr);
        var base64 = UTF8ToString(base64Ptr);

        var binary = atob(base64);
        var len = binary.length;
        var buffer = new Uint8Array(len);
        for (var i = 0; i < len; i++) {
            buffer[i] = binary.charCodeAt(i);
        }

        var blob = new Blob([buffer], { type: "image/png" });
        var url = URL.createObjectURL(blob);

        var a = document.createElement("a");
        a.href = url;
        a.download = filename;
        document.body.appendChild(a);
        a.click();
        document.body.removeChild(a);

        URL.revokeObjectURL(url);
    }
});
