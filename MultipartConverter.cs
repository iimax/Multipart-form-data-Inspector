using HttpMultipartParser;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MultipartFormDataInspector
{
    public class MultipartConverter
    {
        public static List<PostParameter> Convert(byte[] content)
        {
            /*
             ------WebKitFormBoundaryJlYMd1KVllv1WgDS
Content-Disposition: form-data; name="bug"

{"relatedTo":[],"tickets":[],"id":1,"kind":"Other","createdAt":"2014-09-06T08:33:43.614Z","modifiedAt":"2014-09-06T08:33:43.614Z","status":"Verify","title":"Quae inventore beatae tempora mollit deserunt voluptatum odit adipisci consequat Est dolore quia perspiciatis","submittedBy":{"name":"Sudhakar Reddy","email":"sreddy@mycompany.com","username":"sreddy"},"assignTo":{"name":"Guzman Wagner","email":"guzmanwagner@mycompany.com","username":"small"},"description":"sdsdsdsds","category":"MLOS","tofixin":"Help-1.1","severity":"Performance","priority":{"level":"4","title":"Important"},"relation":"Test Specification task for","clones":[],"version":"6.0-3","platform":"EC2","memory":"Reprehenderit quia aut voluptatem in ex dolore eu numquam eum et esse officia id consequatur Est","processors":"Reiciendis nostrum adipisicing occaecat inventore veniam excepturi","note":"Officiis qui adipisci commodo eveniet, esse aperiam est non unde possimus, sed nesciunt, exercitation eius magna consequat. Sint ipsa, laboriosam.","changeHistory":[],"subscribers":[{"name":"Sudhakar Reddy","email":"sreddy@mycompany.com","username":"sreddy"},{"name":"Guzman Wagner","email":"guzmanwagner@mycompany.com","username":"small"}],"attachments":[{"webkitRelativePath":"","lastModifiedDate":"2014-07-18T23:53:29.000Z","name":"jamesbond.jpg","type":"image/jpeg","size":858159}]}
------WebKitFormBoundaryJlYMd1KVllv1WgDS
Content-Disposition: form-data; name="file0"; filename="jamesbond.jpg"
Content-Type: image/jpeg


------WebKitFormBoundaryJlYMd1KVllv1WgDS--
             */
            Stream stream = new MemoryStream(content);
            MultipartFormDataParser parser = null;
            try
            {
                parser = new MultipartFormDataParser(stream);
            }
            catch (Exception)
            {
                return null;
            }
            
            List<PostParameter> lst = new List<PostParameter>();
            foreach (var item in parser.Parameters)
            {
                var pa = new PostParameter();
                pa.Name = item.Name;
                pa.Value = item.Data;
                lst.Add(pa);
            }
            if (parser.Files != null && parser.Files.Count > 0)
            {
                foreach (var item in parser.Files)
                {
                    var para = new PostParameter();
                    para.Name = item.Name;
                    para.Value = string.Format("FileName:{0}, content not displayed", item.FileName);
                    lst.Add(para);
                }
            }
            
            return lst;
        }
    }

    public class PostParameter
    {
        public string Name { get; set; }
        public object Value { get; set; }
    }
}
