// Calculator program
const display = document.getElementById("display");
let isError = false;

function appendToDisplay(input) {
    if (isError) {
        display.value = "";
        isError = false;
    }
    display.value += input;
}

function clearDisplay() {
    display.value = "";
    isError = false;
}

function calculate() {
    try {
        display.value = eval(display.value);
        isError = false;
    } catch (error) {
        display.value = "Error";
        isError = true;
    }

}