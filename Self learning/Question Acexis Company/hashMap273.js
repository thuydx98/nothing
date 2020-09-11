function hashMap(queryType, query) {
    
    var a = [];
    var sum = 0;
    
    for(var i = 0; i < query.length; i++) {
        switch(queryType[i]) {

            case "insert":
                a.push(query[i]);
                break;

            case "addToValue":
                for(var j = 0; j < a.length; j++) {
                    a[j][1] += parseInt(query[i]);
                }
                break;

            case "addToKey":
                for(var j = 0; j < a.length; j++) {
                    a[j][0] += parseInt(query[i]);
                }
                break;
                
            case "get":
                for(var j = 0; j < a.length; j++) {
                    if(a[j][0] == query[i]) {
                        sum += parseInt(a[j][1]);
                        break;
                    }
                }
                break;
        }
    }
    
    return sum;
}