function mutateTheArray(n, a) {
    
    var num1 = 0;
    var num2 = 0;
    var num3 = 0;
    var sum = 0;
    var b = [];
    
    for(var i = 0; i < n; i++) {
        num1 = a[i - 1] == null ? 0 : a[i - 1];
        num2 = a[i] == null ? 0 : a[i];
        num3 = a[i + 1] == null ? 0 : a[i + 1];
        b[i] = num1 + num2 + num3;
    }
    
    return b;
}