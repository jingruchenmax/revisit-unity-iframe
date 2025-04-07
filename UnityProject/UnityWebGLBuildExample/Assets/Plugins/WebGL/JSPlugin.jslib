mergeInto(LibraryManager.library, {
  SendTargetClickedMessage: function(jsonPtr) {
    var json = UTF8ToString(jsonPtr);
    window.parent.postMessage({
      type: "target_click",
      payload: JSON.parse(json)
    }, "*");
  }
});
