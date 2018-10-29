var NavigatorController;

jq(function() {
    NavigatorController = new KeyboardController();

    var emrMessages = {};
    emrMessages["numericRangeHigh"] = "value should be less than {0}";
    emrMessages["numericRangeLow"] = "value should be more than {0}";
    emrMessages["requiredField"] = "Ensure details have been filled properly";
    emrMessages["numberField"] = "Value not a number";
});