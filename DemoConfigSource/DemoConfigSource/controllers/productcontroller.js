import productHandler from '../handlers/producthandler'

export default {
    async create(req, res) {
        try {
            const {
                name,
                price, 
                origin, 
                trademark
            } = req.body
            const newproduct = await productHandler.createproduct(name, price, origin, trademark );
            if(newproduct==true){
                return res.send({
                    data: newproduct,
                    error: null,
                    success: 'ok'
                })
            }else{
                return res.send({
                    data: error,
                    error: null,
                    success: 'ok'
                })
            }
        } catch (error) {
            res.send({
                data: null,
                error: error,
                success: null
            })
        }
    }
}