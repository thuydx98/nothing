function concatenationsSum(a) {
    
    var kq = 0;
   
   if(a.length == 1) {
       kq = parseInt(a[0].toString() + a[0].toString());
   } else {
   
       for(var i = 0; i < a.length; i++) {
           for(var j = 0; j < a.length; j++) {
               kq += parseInt(a[i].toString() + a[j].toString());
           }
       }
   }

   return kq;
}