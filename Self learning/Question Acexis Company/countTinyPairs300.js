function countTinyPairs(a, b, k) {
    
    var kq = 0;
    
    for(var i = 0; i < a.length; i++) {
        if((a[i].toString() + b[a.length - 1 - i].toString()) < k) {
            kq += 1;
        }
    }
    
    return kq;
}