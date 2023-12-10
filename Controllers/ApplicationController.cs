using m21_e2_API.Models;
using m21_e2_API.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Reflection.Metadata;
using static System.Net.WebRequestMethods;

namespace m21_e2_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationController : ControllerBase
    {
        ApplicationContext db;
        UserManager<User> _userManager;
        SignInManager<User> _signInManager;
        
        private readonly IWebHostEnvironment _webHostEnvironment;        
        public ApplicationController(ApplicationContext context, UserManager<User> userManager, SignInManager<User> signInManager, IWebHostEnvironment webHostEnvironment)
        {
            db = context;

            if (!db.SiteTexts.Any())
            {
                db.SiteTexts.Add(new SiteText 
                {
                    MainPage_CompanyDescriptionText = "IT - консалтинг, эстетично и практично!",                   
                    MainPage_RequestButtonText = "ОСТАВИТЬ ЗАЯВКУ",
                    MainPage_RequestText = "Вы можете оставить заявку или задать вопрос в форме ниже",
                    MainPage_RequestAcceptText = "Ваша заявка принята и в скором времени будет обработана. Спасибо за обращение!",
                    MainPage_RequestLinkText = "Заявка",
                    MainPage_MainLinkText = "Главная",
                    MainPage_ServicesLinkText = "Услуги",
                    MainPage_ProjectsLinkText = "Проекты",
                    MainPage_BlogLinkText = "Блог",
                    MainPage_ContactsLinkText = "Контакты",
                    ContactsPage_Address = "Россия, г. Архангельск, Советский пер., д. 11",
                    ContactsPage_ContactsPhone = "+7 (944) 588-49-13",
                    ContactsPage_ContactsFax = "+7 (944) 588-49-13",
                    ContactsPage_ContactsEmail = "kirill.kvartalnov@gmail.com",
                    ContactsPage_ContactsFIO = "Сапалёв Кирилл Павлович"
                });
                db.SaveChanges();
            }

            if (!db.Services.Any())
            {
                db.Services.Add(new Service
                {
                    ServiceTitle = "Проектирование и создание сетевой инфраструктуры",
                    ServiceDescription = "Мы предлагаем нашим заказчикам решения по построению защищенных локальных вычислительных сетей (ЛВС), беспроводных ЛВС, а также распределенных сетей передачи данных. Наши специалисты подберут сетевое оборудование, которое будет отвечать требованиям бизнеса, внедрят его и проведут настройку решения."
                });
                db.Services.Add(new Service
                {
                    ServiceTitle = "Решения для удаленной работы сотрудников",
                    ServiceDescription = "Удаленный формат работы позволяет работать из любой удобной точки земли и не тратить время на дорогу до офиса. Еще одно весомое преимущество – это возможность составить работникам более гибкий график. Однако для максимально эффективной работы необходимы сервисы, которые приблизят дистанционный формат к офлайну."
                });
                db.Services.Add(new Service
                {
                    ServiceTitle = "Серверные и инфраструктурные решения",
                    ServiceDescription = "Мы предлагаем нашим заказчикам комплекс решений и мер по оптимизации IT-инфраструктуры компании. Наши специалисты проведут аудит IT – инфраструктуры в результате которого выделят слабые места и предложат оптимальную схему построения отказоустойчивой и катастрофоустойчивой системы для поддержания всех бизнес – процессов предприятия."
                });
                db.SaveChanges();
            }

            if (!db.Projects.Any())
            {
                db.Projects.Add(new Project
                {
                    ProjectTitle = "Наращивание инфраструктуры",
                    ProjectDescription = "В процессе эксплуатации технические специалисты заказчика или исполнителя следят за состоянием ИТ-инфраструктуры, проводят планово-профилактические работы с оборудованием и программным обеспечением, выполняют регламентные операции, собирают и анализируют обратную связь от пользователей организации и руководителей по использованию. Более подробно об эксплуатации ИТ-инфраструктуры и ее обслуживании описано на странице про ИТ-аутсорсинг.\r\n\r\nЕсли в процессе эксплуатации ИТ-инфраструктуры в организации возникают новые бизнес-процессы, меняются имеющиеся, организация меняется, развиваемся — может возникнуть потребность в модернизации ИТ-инфраструктуры.\r\nПод модернизацией может подразумеваться практически любые изменения ИТ-инфраструктуры, цель которых повысить доступность, безопасность и эффективность ее использования:\r\n\r\n- наращивание мощностей в связи с развитием организации (приобретение компьютеров, серверов, лицензий, дисков, памяти и т.д.);\r\n- внедрение новых систем, служб, сервисов в действующую инфраструктуру в связи с изменениями потребностей бизнеса (средства коллективной работы, CRM, ERP, система документооборота, двухфакторная аутентификация и т.д.);\r\n- внедрение средств защиты информации в связи с изменениями законодательства или появлением новых направлений бизнеса (необходимость обеспечивать сохранность персональных данных, банковской тайна, государственной тайна и т.д.).",
                    PicturePath = "https://localhost:44380/api/application/getpixbyname/proj_1.png"
                });
                db.Projects.Add(new Project
                {
                    ProjectTitle = "Реализация удалённых АРМ",
                    ProjectDescription = "Создание автоматизированных рабочих мест предполагает, что основные операции по \"накоплению, хранению и переработке информации возлагаются на вычислительную технику, а работник сферы управления (экономист, технолог, руководитель и т.д.) выполняет часть ручных операций и операций, требующих творческого подхода при подготовке управленческих решений. Персональная техника применяется пользователем для контроля производственно-хозяйственной деятельности, изменения значений отдельных параметров в ходе решения задачи, а также ввода исходных данных в АИС для решения текущих задач и анализа функций управления.\r\n\r\nАРМ создается для обеспечения выполнения некоторой группы функций. Наиболее простой функцией АРМ является информационно-справочное обслуживание. АРМ имеют проблемно-профессиональную ориентацию на конкретную предметную область.\r\n\r\nПрофессиональные АРМ являются главным инструментом общения человека с вычислительными системами, играя роль автономных рабочих мест, интеллектуальных терминалов больших ЭВМ, рабочих станций в локальных сетях",
                    PicturePath = "https://localhost:44380/api/application/getpixbyname/proj_2.png"
                });
                db.Projects.Add(new Project
                {
                    ProjectTitle = "Аудит систем безопасности",
                    ProjectDescription = "Бизнес все чаще осознает, что для получения новых точек роста в традиционных отраслях требуется привлечение ИТ и систем продвинутой аналитики, которые позволяют получать наиболее полную информацию о поведенческой модели своих клиентов, чтобы получить конкурентное преимущество на рынке. Традиционными инструментами достичь таких целей все сложнее, поэтому компании, даже далекие от ИТ начинают задействовать и развивать внутри своих структур новые направления с привлечением соответствующих специалистов в области продвинутой аналитики данных (Big Data).\r\n\r\nТрансформация традиционного подхода к ведению бизнеса в сторону ИТ– мировая тенденция: сейчас эпоха информационной революции и перехода в digital-среду. Компании осознают, что главное в данном процессе не упустить момент и вовремя начать цифровую трансформацию, которая позволит в ближайшей перспективе получать дополнительный драйвер развития.",
                    PicturePath = "https://localhost:44380/api/application/getpixbyname/proj_3.png"
                });
                db.SaveChanges();
            }

            if (!db.Blogs.Any())
            {
                db.Blogs.Add(new Blog
                {
                    BlogTitle = "От ИТ-аудита к ИТ-консалтингу",
                    BlogDescription = "Эти два понятия часто смешивают. Из-за этого бизнес порой не может понять, какая именно услуга ему нужна сейчас.\r\n\r\nРазобраться в понятиях нам помогут коллеги из финансовой сферы. Там стандарты рынка давно оформились и на уровне правового регулирования, и на уровне понимания со стороны заказчиков.\r\n\r\nНапример, компании предстоит налоговая проверка. Чтобы подготовиться к ней, компания приглашает аудитора. Его задача — проверить, в порядке ли документы, соответствует ли отчётность стандартам, нет ли поводов для штрафов или тем более возбуждения административного (и даже уголовного) дела. И предложить изменения, которые позволят в короткие сроки устранить все несоответствия. Обязанности и полномочия аудитора на этом заканчиваются.\r\n\r\nКонсалтинг находится на уровень выше. Он работает уже не с исправлением последствий, а с выстраиванием правильных процессов. Как работать, чтобы ошибок не возникало? Как достичь бизнес-целей? Как перестроить систему под изменившиеся условия?\r\n\r\nВ разрезе IT ситуация точно такая же: IT-аудит ищет существующие и потенциальные ошибки, IT-консалтинг работает с достижением стратегических и тактических целей в рамках цифровой трансформации. С одной оговоркой: ИТ-консалтинг смотрит на достижение бизнес-целей через призму информационных технологий.",
                    PicturePath = "https://localhost:44380/api/application/getpixbyname/blog_1.png",
                    PublishTime = "29 марта 2023 г."

                });                
                db.Blogs.Add(new Blog
                {
                    BlogTitle = "Уровни консалтинга в IT",
                    BlogDescription = "Нижний уровень — процессный консалтинг. IT-консультант смотрит на процессы, выявляет их слабые места и предлагает конкретные изменения. Он отвечает на вопрос: «Как скорректировать существующие процессы, чтобы получить хороший результат?» Это может быть доработка процессов или систем, а может — внедрение нового функционала.\r\n\r\nНа среднем уровне — в рамках архитектурного консалтинга — главным становится ответ на вопрос: «Как выстроить или перестроить IT-архитектуру компании, чтобы она была наиболее эффективной в рамках существующей бизнес-модели и целей компании?» Это уже работа не с отдельными процессами, а со всей системой деятельности компании. IT-консультант выясняет, кто является держателем каждого из бизнес-процессов в компании, какие проблемы существуют, как IT-архитектура отражает организационный контур компании, а затем дорабатывает существующую архитектуру.\r\n\r\n‍Стратегический консалтинг — это высший уровень, уровень изменения стратегии и цифровой трансформации бизнеса. На этом уровне IT-консалтинг работает не с конкретными технологиями, а со стратегическими решениями и перестройкой бизнес-модели.\r\n\r\nЧем выше уровень консалтинга, тем меньше он сосредоточен на частностях: конкретных системах, которые можно внедрить в ИТ-инфраструктуру компании. Но при этом каждое решение или предложение сильнее отражается на результатах заказчика.\r\n\r\nТут надо отметить, что к ИТ-консалтингу даже на стратегическом уровне бессмысленно приходить с запросом: «Мы не знаем, как жить дальше, но хотим стать „Яндексом\", напишите за нас стратегию развития». Чтобы IT-консалтинг был эффективным, у бизнеса должно быть видение и общая стратегия развития. Консультант лишь помогает «приземлить» их на уровень информационных технологий и цифровизации, сформулировать шаги по реализации стратегии, увидеть потенциальные риски цифровой трансформации и обсудить корректировку стратегии согласно полученному опыту.",
                    PicturePath = "https://localhost:44380/api/application/getpixbyname/blog_2.png",
                    PublishTime = "12 июня 2023 г."
                });
                db.Blogs.Add(new Blog
                {
                    BlogTitle = "Грань между продажами и консалтингом",
                    BlogDescription = "Почти всегда IT-консалтингом занимаются компании, которые одновременно являются и IT-интеграторами. Это накладывает отпечаток на то, каким образом проходит консультация и что является её результатом.\r\n\r\nВладея определёнными технологиями, IT-интегратор часто пытается решить бизнес-задачи заказчиков через призму этих технологий. Так, лет 6–7 назад мы тоже пытались решить запросы заказчиков через Magento — продукт, который на тот момент хорошо освоили.\r\n\r\nЕсли у консультанта в арсенале есть небольшое количество технологий, он ограничен в решениях. И вместо эффективной цифровой трансформации бизнеса рискует загнать заказчика в технологический тупик.\r\n\r\nГораздо продуктивнее вариант, когда IT-консультант имеет опыт работы с широким спектром технологий. В этом случае он может предложить разные подходы, релевантные для бизнес-целей заказчика.\r\n\r\nЕщё один вариант — погоня за «модными технологиями». Блокчейн, компьютерное зрение, машинное обучение сейчас на слуху, вокруг них сложился ореол «крутости». Их воспринимают с энтузиазмом заказчики и разработчики, но реальных успешных кейсов по ним не так много. Предлагая эти технологии с позиции консалтинга, IT-интегратор не всегда учитывает их целесообразность для бизнеса заказчика.\r\n\r\nКонсалтинг не только отвечает на вопрос, что делать, но и объясняет, зачем это делать и каким будет результат. Если консультант может обосновать свои предложения и рекомендации с позиции цифр и целесообразности, его работа была эффективной.",
                    PicturePath = "https://localhost:44380/api/application/getpixbyname/blog_3.png",
                    PublishTime = "3 сентября 2023 г."
                });
                db.SaveChanges();
            }

            if (!db.Contacts.Any())
            {
                db.Contacts.Add(new Contact
                {
                    ContactText = "ВКонтакте",
                    ContactLink = "https://vk.com/SkillProfi",
                    PicturePath = "https://localhost:44380/api/application/getpixbyname/vk.png"
                });
                db.Contacts.Add(new Contact
                {
                    ContactText = "Инстаграм",
                    ContactLink = "https://instagram.com/SkillProfi",
                    PicturePath = "https://localhost:44380/api/application/getpixbyname/instagram.png"
                });
                db.Contacts.Add(new Contact
                {
                    ContactText = "Телеграм",
                    ContactLink = "https://telegram.org/SkillProfi",
                    PicturePath = "https://localhost:44380/api/application/getpixbyname/telegram.png"
                });
                db.SaveChanges();
            }

            if (!db.TagLines.Any())
            {
                db.TagLines.Add(new TagLine
                {
                    TagLineText = "решаем вопросы бизнеса"                    
                });
                db.TagLines.Add(new TagLine
                {
                    TagLineText = "сфера IT - наш профиль"
                });
                db.TagLines.Add(new TagLine
                {
                    TagLineText = "лучшее решение здесь"
                });
                db.SaveChanges();
            }

            if (!db.Requests.Any())
            {               
                db.Requests.Add(new Request
                {
                    RequesterName = "Гремпель Сергей Фадеевич",
                    RequestEmail = "sergey78@rambler.ru",
                    RequestText = "нужно срочно решить проблему с доступом к удалённому АРМ",
                    RequestTime = DateTime.Now.AddDays(-2),
                    RequestStatus = "получена"
                });

                db.Requests.Add(new Request
                {
                    RequesterName = "Ясевич Таисия Николаевна",
                    RequestEmail = "taisiya.yasevich@gmail.com",
                    RequestText = "вы проводите аудит систем ИБ на локальных системах?",
                    RequestTime = DateTime.Now.AddDays(-30),
                    RequestStatus = "получена"
                });

                db.Requests.Add(new Request
                {
                    RequesterName = "Митрофанов Филипп Себастьянович",
                    RequestEmail = "filipp20091965@rambler.ru",
                    RequestText = "мне необходима консультация, я просто спросить",
                    RequestTime = DateTime.Now.AddDays(-100),
                    RequestStatus = "получена"
                });             

                db.SaveChanges();                
            }
            _userManager = userManager;
            _signInManager = signInManager;
            _webHostEnvironment = webHostEnvironment;            
        }        

        //ТЕКСТЫ САЙТА----------------------------------------------------
        //получить все тексты сайта
        [HttpGet("sitetext")]
        public async Task<ActionResult<SiteText>> GetSiteText()
        {
            SiteText siteText = await db.SiteTexts.FirstOrDefaultAsync(x => x.Id == 1);
            if (siteText == null)
            {
                return NotFound();
            }
            return Ok(siteText);
        }

        //изменение текстов главной страницы
        [HttpPut("sitetext")]
        public async Task<IActionResult> PutSiteText(MainClass mc)
        {
            SiteText st = await db.SiteTexts.FirstOrDefaultAsync(x => x.Id == 1);
            st.MainPage_CompanyDescriptionText = mc.siteText.MainPage_CompanyDescriptionText;
            st.MainPage_RequestButtonText = mc.siteText.MainPage_RequestButtonText;
            st.MainPage_RequestText = mc.siteText.MainPage_RequestText;
            st.MainPage_MainLinkText = mc.siteText.MainPage_MainLinkText;
            st.MainPage_ServicesLinkText = mc.siteText.MainPage_ServicesLinkText;
            st.MainPage_ProjectsLinkText = mc.siteText.MainPage_ProjectsLinkText;
            st.MainPage_BlogLinkText = mc.siteText.MainPage_BlogLinkText;
            st.MainPage_ContactsLinkText = mc.siteText.MainPage_ContactsLinkText;
            db.SiteTexts.Update(st);
            db.TagLines.Update(mc.tagLineList[0]);
            db.TagLines.Update(mc.tagLineList[1]);
            db.TagLines.Update(mc.tagLineList[2]);
            var result = await db.SaveChangesAsync();
            if (result == 0)
            {
                return BadRequest();
            }
            return Ok();
        }

        //получить все слоганы
        [HttpGet("tagline")]
        public async Task<ActionResult<IEnumerable<TagLine>>> GetTagLines()
        {
            return await db.TagLines.ToListAsync();
        }

        //получить слоган по Id
        [HttpGet("tagline/{id}")]
        public async Task<ActionResult<TagLine>> GetTagLine(int id)
        {
            TagLine request = await db.TagLines.FirstOrDefaultAsync(x => x.Id == id);
            if (request == null)
            {
                return NotFound();
            }
            return Ok(request);
        }

        //получить произвольный слоган по Id
        [HttpGet("randomtagline")]
        public async Task<ActionResult<IEnumerable<TagLine>>> GetRandomTagLine()
        {
            Random rand = new Random();
            int r = rand.Next(1, 4);
            TagLine request = await db.TagLines.FirstOrDefaultAsync(x => x.Id == r);
            if (request == null)
            {
                return NotFound();
            }
            return Ok(request);
        }

        //ЗАЯВКИ------------------------------------------------------------
        //получить все заявки
        [HttpGet("request")]
        public async Task<ActionResult<List<Request>>> GetRequest()
        {
            return await db.Requests.ToListAsync();
        }
                
        //получить заявку по Id
        [HttpGet("request/{id}")]
        public async Task<ActionResult<Request>> GetRequest(int id)
        {
            Request request = await db.Requests.FirstOrDefaultAsync(x => x.Id == id);
            if (request == null)
            {
                return NotFound();
            }
            return Ok(request);
        }

        //добавить заявку
        [HttpPost("request")]
        public async Task<IActionResult> PostRequest(RequestViewModel request)
        {
            Request newReq = new Request();
            newReq.RequesterName = request.RequesterName;
            newReq.RequestEmail = request.RequestEmail;
            newReq.RequestText = request.RequestText;
            newReq.RequestTime = DateTime.Now;
            newReq.RequestStatus = "получена";
            await db.Requests.AddAsync(newReq);
            var result = await db.SaveChangesAsync();
            if (result == 0)
            {
                return BadRequest();
            }
            return Ok();
        }

        //изменить статус заявки
        [HttpPut("request")]
        public async Task<IActionResult> PutRequest(Request request)
        {
            db.Requests.Update(request);
            var result = await db.SaveChangesAsync();
            if (result == 0)
            {
                return BadRequest();
            }
            return Ok();
        }

        //УСЛУГИ------------------------------------------------------------
        //получить все услуги
        [HttpGet("services")]
        public async Task<ActionResult<IEnumerable<Service>>> GetServices()
        {
            return await db.Services.ToListAsync();
        }

        //получить услугу по Id
        [HttpGet("services/{id}")]
        public async Task<ActionResult<Service>> GetService(int id)
        {
            Service service = await db.Services.FirstOrDefaultAsync(x => x.Id == id);
            if (service == null)
            {
                return NotFound();
            }
            return Ok(service);
        }

        //добавить услугу
        [HttpPost("services")]
        public async Task<IActionResult> PostService(Service service)
        {
            await db.Services.AddAsync(service);
            var result = await db.SaveChangesAsync();
            if (result == 0)
            {
                return BadRequest();
            }
            return Ok();
        }

        //удалить услугу
        [HttpDelete("services/{id}")]
        public async Task<IActionResult> DeleteService(int id)
        {
            Service service = await db.Services.FirstOrDefaultAsync(x => x.Id == id);
            db.Services.Remove(service);
            var result = await db.SaveChangesAsync();
            if (result == 0)
            {
                return BadRequest();
            }
            return Ok();
        }

        //изменить услугу
        [HttpPut("services")]
        public async Task<IActionResult> PutService(Service service)
        {            
            db.Services.Update(service);
            var result = await db.SaveChangesAsync();
            if (result == 0)
            {
                return BadRequest();
            }
            return Ok();
        }


        //ПРОЕКТЫ------------------------------------------------------------
        //получить все проекты
        [HttpGet("projects")]
        public async Task<ActionResult<IEnumerable<Project>>> GetProjects()
        {
            return await db.Projects.ToListAsync();
        }

        //получить проект по Id
        [HttpGet("projects/{id}")]
        public async Task<ActionResult<Project>> GetProject(int id)
        {
            Project project = await db.Projects.FirstOrDefaultAsync(x => x.Id == id);
            if (project == null)
            {
                return NotFound();
            }
            return Ok(project);
        }

        //добавить проект
        [HttpPost("projects")]
        public async Task<IActionResult> PostProject([FromForm] ProjectViewModel project)
        {
            if (project.PictureFile.Length > 0)
            {
                string path = @$"wwwroot\\pix\\";
                string fileName = Path.GetRandomFileName().Replace(".", string.Empty) + Path.GetExtension(project.PictureFile.FileName);
                using (var stream = new FileStream(path + fileName, FileMode.Create))
                {
                    await project.PictureFile.CopyToAsync(stream);
                }
                var siteFilePath = $"{Request.Scheme}://{Request.Host.Value}/api/application/getpixbyname/" + fileName;
                await db.Projects.AddAsync(
                    new Project
                    {
                        ProjectTitle = project.ProjectTitle,
                        ProjectDescription = project.ProjectDescription,
                        PicturePath = siteFilePath
                    });
                var result = await db.SaveChangesAsync();
                if (result == 0)
                {
                    return BadRequest();
                }
                return Ok();
            }
            return BadRequest();
        }

        //удалить проект
        [HttpDelete("projects/{id}")]
        public async Task<IActionResult> DeleteProject(int id)
        {
            Project project = await db.Projects.FirstOrDefaultAsync(x => x.Id == id);
            db.Projects.Remove(project);
            var result = await db.SaveChangesAsync();
            if (result == 0)
            {
                return BadRequest();
            }
            return Ok();
        }

        //редактировать проект с заменой картинки
        [HttpPut("projects")]
        public async Task<IActionResult> PutProject([FromForm] ProjectViewModel project)
        {
            if (project.PictureFile.Length > 0)
            {
                string path = @$"wwwroot\\pix\\";
                string fileName = Path.GetRandomFileName().Replace(".", string.Empty) + Path.GetExtension(project.PictureFile.FileName);
                using (var stream = new FileStream(path + fileName, FileMode.Create))
                {
                    await project.PictureFile.CopyToAsync(stream);
                }
                var siteFilePath = $"{Request.Scheme}://{Request.Host.Value}/api/application/getpixbyname/" + fileName;
                Project upProj = new Project
                {
                    Id = (int)project.Id,
                    ProjectTitle = project.ProjectTitle,
                    ProjectDescription = project.ProjectDescription,
                    PicturePath = siteFilePath                    
                };
                db.Projects.Update(upProj);
                var result = await db.SaveChangesAsync();
                if (result == 0)
                {
                    return BadRequest();
                }
                return Ok();
            }
            return BadRequest();
        }

        //редактировать проект без замены картинки
        [HttpPut("projectsnonepix")]
        public async Task<IActionResult> PutProjectNonePix([FromForm] ProjectViewModel projectvm)
        {
            Project project = await db.Projects.FirstOrDefaultAsync(x => x.Id == projectvm.Id);
            project.ProjectTitle = projectvm.ProjectTitle;
            project.ProjectDescription = projectvm.ProjectDescription;
            db.Projects.Update(project);
            var result = await db.SaveChangesAsync();
            if (result == 0)
            {
                return Ok(result);
            }
            return Ok();
        }

        //БЛОГ------------------------------------------------------------
        //получить все блоги
        [HttpGet("blog")]
        public async Task<ActionResult<IEnumerable<Blog>>> GetBlogs()
        {
            return await db.Blogs.ToListAsync();
        }

        //получить блог по Id
        [HttpGet("blog/{id}")]
        public async Task<ActionResult<Blog>> GetBlog(int id)
        {
            Blog blog = await db.Blogs.FirstOrDefaultAsync(x => x.Id == id);
            if (blog == null)
            {
                return NotFound();
            }
            return Ok(blog);
        }

        //добавить блог
        [HttpPost("blog")]
        public async Task<IActionResult> PostBlog([FromForm] BlogViewModel blog)
        {
            if (blog.PictureFile.Length > 0)
            {
                string path = @$"wwwroot\\pix\\";
                string fileName = Path.GetRandomFileName().Replace(".", string.Empty) + Path.GetExtension(blog.PictureFile.FileName);
                using (var stream = new FileStream(path + fileName, FileMode.Create))
                {
                    await blog.PictureFile.CopyToAsync(stream);
                }

                var siteFilePath = $"{Request.Scheme}://{Request.Host.Value}/api/application/getpixbyname/" + fileName;
                await db.Blogs.AddAsync(
                    new Blog
                    {
                        BlogTitle = blog.BlogTitle,
                        BlogDescription = blog.BlogDescription,
                        PicturePath = siteFilePath,
                        PublishTime = DateTime.Now.ToString("D")
                    }) ;
                var result = await db.SaveChangesAsync();
                if (result == 0)
                {
                    return BadRequest();
                }
                return Ok();
            }
            return BadRequest();
        }

        //удалить блог
        [HttpDelete("blog/{id}")]
        public async Task<IActionResult> DeleteBlog(int id)
        {
            Blog blog = await db.Blogs.FirstOrDefaultAsync(x => x.Id == id);
            db.Blogs.Remove(blog);
            var result = await db.SaveChangesAsync();
            if (result == 0)
            {
                return BadRequest();
            }
            return Ok();
        }

        //редактировать блог с заменой картинки
        [HttpPut("blog")]
        public async Task<IActionResult> PutBlog([FromForm] BlogViewModel blog)
        {            
            string path = @$"wwwroot\\pix\\";
            string fileName = Path.GetRandomFileName().Replace(".", string.Empty) + Path.GetExtension(blog.PictureFile.FileName);
            using (var stream = new FileStream(path + fileName, FileMode.Create))
            {
                await blog.PictureFile.CopyToAsync(stream);
            }
            var siteFilePath = $"{Request.Scheme}://{Request.Host.Value}/api/application/getpixbyname/" + fileName;
            Blog upBlog = new Blog
            {
                Id = (int)blog.Id,
                BlogTitle = blog.BlogTitle,
                BlogDescription = blog.BlogDescription,
                PicturePath = siteFilePath,
                PublishTime = DateTime.Now.ToString("D")
            };

            db.Blogs.Update(upBlog);
            var result = await db.SaveChangesAsync();
            if (result == 0)
            {
                return BadRequest();
            }
            return Ok();
        }

        //редактировать блог без замены картинки
        [HttpPut("blognonepix")]
        public async Task<IActionResult> PutBlogNonePix([FromForm] BlogViewModel blogvm)
        {
            Blog blog = await db.Blogs.FirstOrDefaultAsync(x => x.Id == blogvm.Id);            
            blog.BlogTitle = blogvm.BlogTitle;
            blog.BlogDescription = blogvm.BlogDescription;
            db.Blogs.Update(blog);
            var result = await db.SaveChangesAsync();
            if (result == 0)
            {
                return Ok(result);
            }
            return Ok();
        }

        //КОНТАКТЫ------------------------------------------------------------
        //получить все контакты
        [HttpGet("contacts")]
        public async Task<ActionResult<List<Contact>>> GetContactss()
        {
            return await db.Contacts.ToListAsync();
        }

        //добавить контакт
        [HttpPost("contacts")]
        public async Task<IActionResult> PostContact([FromForm] ContactViewModel con)
        {
            string path = @$"wwwroot\\pix\\";
            string fileName = Path.GetRandomFileName().Replace(".", string.Empty) + Path.GetExtension(con.PictureFile.FileName);
            using (var stream = new FileStream(path + fileName, FileMode.Create))
            {
                await con.PictureFile.CopyToAsync(stream);
            }
            var siteFilePath = $"{Request.Scheme}://{Request.Host.Value}/api/application/getpixbyname/" + fileName;
            await db.Contacts.AddAsync(
                new Contact
                {
                    ContactText = con.ContactText,
                    ContactLink = con.ContactLink,
                    PicturePath = siteFilePath                    
                });
            var result = await db.SaveChangesAsync();
            if (result == 0)
            {
                return BadRequest();
            }
            return Ok();
        }

        //удалить контакт
        [HttpDelete("contacts/{id}")]
        public async Task<IActionResult> DeleteContact(int id)
        {
            Contact con = await db.Contacts.FirstOrDefaultAsync(x => x.Id == id);
            db.Contacts.Remove(con);
            var result = await db.SaveChangesAsync();
            if (result == 0)
            {
                return BadRequest();
            }
            return Ok();
        }

        //изменение текстов страницы контактов
        [HttpPut("contactstexts")]
        public async Task<IActionResult> PutContactsText(MainClass mc)
        {
            SiteText st = await db.SiteTexts.FirstOrDefaultAsync(x => x.Id == 1);
            st.ContactsPage_Address = mc.siteText.ContactsPage_Address;
            st.ContactsPage_ContactsPhone = mc.siteText.ContactsPage_ContactsPhone;
            st.ContactsPage_ContactsFax = mc.siteText.ContactsPage_ContactsFax;
            st.ContactsPage_ContactsEmail = mc.siteText.ContactsPage_ContactsEmail;
            st.ContactsPage_ContactsFIO = mc.siteText.ContactsPage_ContactsFIO;
            db.SiteTexts.Update(st);
            var result = await db.SaveChangesAsync();
            if (result == 0)
            {
                return BadRequest();
            }
            return Ok();
        }

        //редактировать контакт без замены картинки
        [HttpPut("contactnonepix")]
        public async Task<IActionResult> PutContactLinkNonePix([FromForm] ContactViewModel contactvm)
        {
            Contact contact = await db.Contacts.FirstOrDefaultAsync(x => x.Id == contactvm.Id);            
            contact.ContactText = contactvm.ContactText;
            contact.ContactLink = contactvm.ContactLink;                         
            db.Contacts.Update(contact);
            var result = await db.SaveChangesAsync();
            if (result == 0)
            {
                return Ok(result);
            }
            return Ok();
        }

        //редактировать контакт с заменой картинки
        [HttpPut("contactwithpix")]
        public async Task<IActionResult> PutContactLinkWithPix([FromForm] ContactViewModel contact)
        {
            Contact con = await db.Contacts.FirstOrDefaultAsync(x => x.Id == contact.Id);            
            con.ContactText = contact.ContactText;
            con.ContactLink = contact.ContactLink;
            string path = @$"wwwroot\\pix\\";
            string fileName = Path.GetRandomFileName().Replace(".", string.Empty) + Path.GetExtension(contact.PictureFile.FileName);
            using (var stream = new FileStream(path + fileName, FileMode.Create))
            {
                await contact.PictureFile.CopyToAsync(stream);
            }
            var siteFilePath = $"{Request.Scheme}://{Request.Host.Value}/api/application/getpixbyname/" + fileName;
            con.PicturePath = siteFilePath;
            db.Contacts.Update(con);
            var result = await db.SaveChangesAsync();
            if (result == 0)
            {
                return Ok(result);
            }
            return Ok();
        }


        //ПОЛУЧЕНИЕ КАРТИНКИ------------------------------------------------------------
        //получить картинку по имени файла
        [HttpGet("getpixbyname/{name}")]
        public async Task<IActionResult> GetPixByName(string name)
        {
            string filePath = Path.Combine(_webHostEnvironment.ContentRootPath, $"wwwroot/pix/{name}");
            return PhysicalFile(filePath, "image/jpeg");
        }

        //получить картинку по Id
        [HttpGet("getpix/{id}")]
        public async Task<IActionResult> GetPix(int id)
        {
            string url = @$"wwwroot\\pix\\pix{id}.png";
            string filePath = Path.Combine(_webHostEnvironment.ContentRootPath, $"wwwroot/pix/pix{id}.png");
            return PhysicalFile(filePath, "image/jpeg");
        }

                
        //USERS----------------------------------------------------
        //получение всех пользователей
        [HttpGet("users")]
        public async Task<ActionResult<List<User>>> Get()
        {
            return await _userManager.Users.ToListAsync();
        }

        //получение одного конкретного пользователя
        [HttpGet("users/{id}")]
        public async Task<ActionResult<User>> Get(string id)
        {
            User user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        //добавление пользователя
        [HttpPost("users")]
        public async Task<IActionResult> Post(UserForm reg)
        {
            User user = new User()
            {
                UserName = reg.UserName
            };
            var result = await _userManager.CreateAsync(user, reg.Password);
            if (!result.Succeeded)
            {
                return BadRequest();
            }
            return Ok();
        }

        //удаление пользователя
        [HttpDelete("users/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            User user = await _userManager.FindByIdAsync(id);
            var result = await _userManager.DeleteAsync(user);
            if (!result.Succeeded)
            {
                return BadRequest();
            }
            return Ok();
        }

        //изменение пользователя
        [HttpPut("users")]
        public async Task<IActionResult> Put(User user)
        {
            User userUpdate = await _userManager.FindByIdAsync(user.Id);
            userUpdate.UserName = user.UserName;
            var result = await _userManager.UpdateAsync(userUpdate);

            if (!result.Succeeded)
            {
                return BadRequest();
            }
            return Ok();
        }

        //LOGIN----------------------------------------------------
        [HttpPost("login")]
        public async Task<IActionResult> Login(UserForm user)
        {
            var result = await _signInManager.PasswordSignInAsync(user.UserName, user.Password, true, false);

            if (!result.Succeeded)
            {
                return BadRequest();
            }
            return Ok();
        }

        //REGISTER----------------------------------------------------        
        [HttpPost("register")]
        public async Task<IActionResult> Registration(UserForm reg)
        {
            User user = new User()
            {
                UserName = reg.UserName
            };
            var result = await _userManager.CreateAsync(user, reg.Password);

            if (!result.Succeeded)
            {
                return BadRequest();
            }
            return Ok();
        }
    }
}
