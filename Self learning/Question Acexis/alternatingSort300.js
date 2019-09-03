function alternatingSort(a) {
    
    if(a.length == 1) {
        return true;
    }
    
    for(var i = 0; i < parseInt(a.length / 2); i++) {
        if(a[i] >= a[a.length - 1 - i] || a[i] <= a[a.length - i]) {
            return false;
        }
    }
    
    if(a.length % 2 == 1 && a[a.length / 2 - 0.5 ] <= a[a.length / 2 + 0.5 ]) {
        return false;        
    }
    
    return true;
}
