$("body")
  .on("keydown", ".numeric-only", function () {
    var e = event;
    if (
      $.inArray(e.keyCode, [46, 8, 9, 27, 13, 110]) !== -1 ||
      (e.keyCode === 65 && (e.ctrlKey === true || e.metaKey === true)) ||
      ((e.keyCode === 88 || e.keyCode == 67) &&
        (e.ctrlKey === true || e.metaKey === true))
    ) {
      return;
    }
    if (
      (e.shiftKey || e.keyCode < 48 || e.keyCode > 57) &&
      (e.keyCode < 96 || e.keyCode > 105)
    ) {
      e.preventDefault();
    }
  })
  .on("paste", ".numeric-only", function () {
    event.preventDefault();
  });
