import teacherhandler from '../handlers/teacherhandler'

export default{
    async create(req,res){
        try {
            const result = teacherhandler.createteacher(req.body.name, req.body.id, req.body.age, req.body.email, req.body.phone);
            console.log(result);
            return res.send({
                data: result,
                error: null,
                success: 'ok'
            }) 
        } catch (error) {
            return res.send({
                date: null,
                error: error,
                success: null
            })
        }
    }
}