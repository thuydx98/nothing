function mergeStrings(s1, s2) {
    
    var result = "";
    var saveWord1 = s1;
    var saveWord2 = s2;
    
    var total = s1.length + s2.length;
    
    for(var i = 0; i < total; i++) {
        if(s1.length == "") {
            result += s2;
            break;
        } else if(s2.length == "") {
            result += s1;
            break;
        } else {
            
            var totalS1 = calculate(s1[0], saveWord1);
            var totalS2 = calculate(s2[0], saveWord2);
            
            if(totalS1 < totalS2) {
                result += s1[0];
                s1 = s1.substr(1);
            } else if(totalS1 > totalS2) {
                result += s2[0];
                s2 = s2.substr(1);
            } else if(s1[0] > s2[0]) {
                result += s2[0];
                s2 = s2.substr(1);  
            } else {
                result += s1[0];
                s1 = s1.substr(1);
            }   
        }
    }
    
    console.log(result);
    return result;
}

function calculate(symbol, word) {
    
    var sum = 0;
    
    for(var i = 0; i < word.length; i++) {
        if(symbol == word[i]) {
            sum++;
        }
    }
    
    return sum;
}