function meanGroups(a) {
    
    var arrArg = [];
    var kq = [];
    
    for(var i = 0; i < a.length; i++) {
        
        var sum = 0;
        var arg = 0;
        
        for(var j = 0; j < a[i].length; j++) {
            sum += a[i][j];
        }
        
        arg = sum / a[i].length;
        arrArg.push(arg);
    }
    
    console.log(arrArg);
    
    for(var i = 0; i < arrArg.length; i++) {
        if(arrArg[i] !== "x") {
            var temp = [];
            temp.push(i);
            
            for(var j = i + 1; j < arrArg.length; j++) {
                if(arrArg[j] == arrArg[i]) {
                    temp.push(j);
                    arrArg[j] = "x";
                }
            }
            
            kq.push(temp);
        }
    }

    return kq;
}