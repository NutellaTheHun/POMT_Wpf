import pdfToPrinter from 'pdf-to-printer';
const { print } = pdfToPrinter;
const { getPrinters } = pdfToPrinter;
function Print4x2(filePath, copies) {
    console.log(filePath);
    const options = {
        printer: "Brother HL-2280DW Printer",
        paperSize: "letter",
        //copies: parseInt(copies, 10)
        copies:1
    };
    //getPrinters().then(console.log);
    
    print(filePath, options)
        .then(console.log)
        .catch(console.error);
        
}
function Print2x1(filePath, copies) {
    const options = {
        printer: "Rollo",
        paperSize:"2x1",
        copies: parseInt(copies, 10)
    };
    print(filePath, options)
        .then(console.log)
        .catch(console.error);;
}

// Get arguments from command line
const [, , filePath, copies] = process.argv;

if (filePath && copies) {
    Print4x2(filePath, copies);
} else {
    console.error("File path and copies are required.");
}
