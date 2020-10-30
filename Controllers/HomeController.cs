using FilmsApp.Models;
using FilmsApp.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FilmsApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationContext db;
        private readonly IHostEnvironment env;
        private readonly UserManager<User> userManager;

        public HomeController(ApplicationContext _context, UserManager<User> _userManager, SignInManager<User> _signInManager, IHostEnvironment _environment)
        {
            db = _context;
            env = _environment;
            userManager = _userManager;

            if (db.Films.Count() == 0)
            {
                //здесь будет 15 фильмов
                Film film1 = new Film { Id = "guid1", Name = "Птичий короб", About = "Пять лет назад мир погрузился в хаос: увидев нечто, люди кончают жизнь самоубийством. Женщина с двумя пятилетними детьми, услышав по радио о безопасном месте, отправляется на поиски выжившей общины и прихватывает с собой попугаев в коробке. Чтобы сохранить жизнь в этом новом мире, самое главное — не смотреть по сторонам и слушать, как птицы реагируют на приближающуюся опасность.", Year = 2018, Author = "Сюзанна Бир", Creator = "testUserId", PosterPath = "/posters/guid1/poster.webp" };
                Film film2 = new Film { Id = "guid2", Name = "Шафт", About = "Джон Шафт III — специалист по кибербезопасности в ФБР, не привыкший решать проблемы грубой силой. Он предпочитает полагаться на логику и расчёт, но убийство лучшего друга ставит его в тупик. С ним не разобраться современными методами, так что Джону ничего не остается, кроме как обратиться за помощью к старшему поколению Шафтов, чьи старые методы помогут ему найти убийцу.", Year = 2018, Author = "Тим Стори", Creator = "testUserId", PosterPath = "/posters/guid2/poster.webp" };
                Film film3 = new Film { Id = "guid3", Name = "El Camino: Во все тяжкие", About = "Джесси Пинкман сбежал от неонацистов. Не зная, куда ему податься, он скрывается от полиции, похитителей и прошлого. Теперь он должен понять, как ему жить дальше.", Year = 2019, Author = "Винс Гиллиган", Creator = "testUserId", PosterPath = "/posters/guid3/poster.webp" };
                Film film4 = new Film { Id = "guid4", Name = "В погоне за Бонни и Клайдом", About = "Пять лет назад мир погрузился в хаос: увидев нечто, люди кончают жизнь самоубийством. Женщина с двумя пятилетними детьми, услышав по радио о безопасном месте, отправляется на поиски выжившей общины и прихватывает с собой попугаев в коробке. Чтобы сохранить жизнь в этом новом мире, самое главное — не смотреть по сторонам и слушать, как птицы реагируют на приближающуюся опасность.", Year = 2019, Author = "Джон Ли Хэнкок", Creator = "testUserId", PosterPath = "/posters/guid4/poster.webp" };
                Film film5 = new Film { Id = "guid5", Name = "Яркость", About = "В альтернативном Лос-Анджелесе кого только не встретишь - бок о бок с людьми живут эльфы, орки и даже кентавры. Эльфы, правда, немного брезгуют всеми остальными, поэтому устроили себе отдельный район, куда въезд только по пропускам. А вот людям приходится терпеть грубых и склонных к преступлениям орков, те предпочитают селиться в криминальных гетто.", Year = 2017, Author = "Дэвид Эйр", Creator = "testUserId", PosterPath = "/posters/guid5/poster.webp" };
                Film film6 = new Film { Id = "guid6", Name = "Мальчик, который обуздал ветер", About = "Мальчик, живущий в бедной африканской деревушке в Малави, помогает взрослым строить ветряную мельницу, черпая знания из библиотечной книги.", Year = 2019, Author = "Чиветель Эджиофор", Creator = "testUserId", PosterPath = "/posters/guid6/poster.webp" };
                Film film7 = new Film { Id = "guid7", Name = "Окча", About = "Корпорация «Мирандо» создала необычных поросят и раздала их фермерам по всему миру. На протяжении десяти лет гигантская экспериментальная свинья Окча была лучшим другом девочки Ми-джа, они мирно жили в южнокорейских горах и заботились друг о друге. Но в один ужасный день идиллическая жизнь друзей прервалась - корпорация забрала свою собственность. У стервозной генеральной директрисы Люси Мирандо свои, не самые гуманные, планы на необычного зверя. Конечно же, храбрая девочка не бросит друга в беде, и Ми-джа отправляется спасать Окчу, но мир оказывается намного безумней, чем она могла себе представить.", Year = 2017, Author = "Пон Джун-хо", Creator = "testUserId", PosterPath = "/posters/guid7/poster.webp" };
                Film film8 = new Film { Id = "guid8", Name = "Божественные", About = "15-летняя гопница Дуния из парижского гетто жаждет власти и успеха. Заручившись поддержкой своей лучшей подруги Маймуны, она решает влиться в сеть наркоторговцев. Но когда Дуния встречает молодого танцовщика Дигия, для неё приоткрывается дверь в совершенно иную жизнь.", Year = 2016, Author = "Уда Беньямина", Creator = "testUserId", PosterPath = "/posters/guid8/poster.webp" };
                Film film9 = new Film { Id = "guid9", Name = "Наши души по ночам", About = "Двое пожилых овдовевших соседей начинают платонически спать друг с другом, чтобы побороть одиночество.", Year = 2017, Author = "Ритеш Батра", Creator = "testUserId", PosterPath = "/posters/guid9/poster.webp" };
                Film film10 = new Film { Id = "guid10", Name = "Убийство на яхте", About = "После 15 лет брака американская супружеская пара Ник и Одри Шпиц, полицейский и парикмахерша, наконец-то отправляется в отпуск в Европу. В самолёте супруга умудряется познакомиться с английским аристократом, и тот приглашает их с мужем провести время на яхте дяди - известного миллиардера. Супруги Шпиц оказываются в высшем обществе в окружении невиданной роскоши, когда вдруг престарелого главу семейства убивают при загадочных обстоятельствах. Мотивы есть у всех присутствующих, но французский детектив решает, что главные подозреваемые - американцы, при этом большая любительница детективов Одри видит в этой ситуации главное приключение своей жизни.", Year = 2019, Author = "Кайл Ньюачек", Creator = "testUserId", PosterPath = "/posters/guid10/poster.webp" };
                Film film11 = new Film { Id = "guid11", Name = "Перелом", About = "Рэй с женой и маленькой дочерью едет на машине к родителям на День благодарения. На очередной остановке девочка, испугавшись собаки, неудачно пятится и падает с небольшой высоты. Рэй пытается поймать дочь, прыгает за ней и, приземлившись ниц, теряет сознание. Он приходит в себя от криков жены и, справившись с головокружением, спешно везёт дочь в больницу. После ожидания в очереди и скандала с регистратурой взволнованные родители добиваются осмотра врача, который рекомендует сделать МРТ. Отправив жену с дочерью на обследование, Рэй засыпает в кресле в фойе, а когда просыпается, выясняет, что жена и дочь бесследно исчезли — вместе с записью в журнале приёма.", Year = 2019, Author = "Брэд Андерсон", Creator = "testUserId", PosterPath = "/posters/guid11/poster.webp" };
                Film film12 = new Film { Id = "guid12", Name = "Рождественские хроники", About = "В канун Рождества брат с сестрой - Кейт и Тедди Пирс - собираются заснять на видео появление Санты. Дерзкий замысел выливается в необыкновенное приключение, о котором дети не могли даже мечтать. Вместе с преданными эльфами и волшебными летающими оленями они помогают Санте спасти всеми любимый праздник.", Year = 2018, Author = "Клэй Кэтис", Creator = "testUserId", PosterPath = "/posters/guid12/poster.webp" };
                Film film13 = new Film { Id = "guid13", Name = "Два Папы", About = "2005 год. После смерти Иоанна Павла II кардиналы католической церкви собираются в Ватикане, чтобы провести голосование и выбрать нового папу. В первом туре больше остальных голосов набирают консервативный кардинал Ратцингер и кардинал Бергольо, сторонник реформирования. По итогам повторного голосования главой католической церкви выбирают Ратцингера - он становится Бенедиктом XVI.", Year = 2019, Author = "Фернанду Мейреллиш", Creator = "testUserId", PosterPath = "/posters/guid13/poster.webp" };
                Film film14 = new Film { Id = "guid14", Name = "Грязь", About = "История американской рок-группы Mötley Crüe.", Year = 2019, Author = "Джефф Треймейн", Creator = "testUserId", PosterPath = "/posters/guid14/poster.webp" };
                Film film15 = new Film { Id = "guid15", Name = "Кафе «Голубая сойка»", About = "Джим и Аманда были первой любовью друг для друга еще в школе. Расставшись, каждый пошёл своей дорогой, покинул дом и отправился искать своё счастье, призвание и судьбу. Но вот спустя двадцать лет Джим возвращается в родной провинциальный городок, чтобы привести в порядок родительский дом перед предстоящей продажей, и надо же такому случиться, что Аманда тоже заглянула в город, чтобы проведать беременную сестру. В одно и то же самое время они заходят в местный продуктовый магазинчик и сталкиваются лбами. Глазам не могут поверить, но быстро приходят в себя, решают выпить по чашечке кофе и вспомнить былые времена…", Year = 2016, Author = "Александра Леманн", Creator = "testUserId", PosterPath = "/posters/guid15/poster.webp" };

                db.Films.AddRange(film1, film2, film3, film4, film5, film6, film7, film8, film9, film10, film11, film12, film13, film14, film15);
                db.SaveChanges();
            }
        }

        //public async Task<IActionResult> Index() => View(await db.Films.ToListAsync());

        public async Task<IActionResult> Index(int page=1)
        {
            int pageSize = 6;
            IQueryable<Film> source = db.Films;
            var count = await source.CountAsync();
            var items = await source.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
            IndexViewModel viewModel = new IndexViewModel
            {
                PageViewModel = pageViewModel,
                Films = items
            };
            return View(viewModel);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(FilmViewModel film)
        {
            var newId = Guid.NewGuid().ToString();
            var file = film.Poster;
            var upload = Path.Combine(env.ContentRootPath, "wwwroot\\posters", newId);

            if (!Directory.Exists(upload))
                Directory.CreateDirectory(upload);

            var filePath = Path.Combine(upload, file.FileName);

            // временная мера
            var localPath = "/posters/" + newId + "/" + file.FileName; 


            if (file.Length > 0)
            {
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }
            }

            var newFilm = new Film() {
                Id = newId,
                Name = film.Name,
                About = film.About,
                Year = film.Year,
                Author = film.Author,
                Creator = userManager.GetUserId(User),
                PosterPath = localPath
            };

            if (userManager.GetUserId(User) != null)
            {
                db.Films.Add(newFilm);
                await db.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Details(string id)
        {
            Film film = await db.Films.FirstOrDefaultAsync(x => x.Id == id);
            return View(film);
        }

        public async Task<IActionResult> Edit(string id)
        {
            Film film = await db.Films.FirstOrDefaultAsync(x => x.Id == id);

            var upload = Path.Combine(env.ContentRootPath, "wwwroot\\posters", film.Id);
            DirectoryInfo path = new DirectoryInfo(upload);
            var file = path.GetFiles()[0];
            var fs = file.OpenRead();
            FormFile ff = new FormFile(fs,0,fs.Length,film.PosterPath,file.Name);


            var newFilm = new FilmViewModel()
            {
                Id = film.Id,
                Name = film.Name,
                About = film.About,
                Year = film.Year,
                Author = film.Author,
                Creator = film.Creator,
                Poster = ff
            };

            return View(newFilm);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string id, FilmViewModel film)
        {
            Film newFilm = await db.Films.FirstOrDefaultAsync(x => x.Id == id);

            var file = film.Poster;

            if (file != null)
            {
                var upload = Path.Combine(env.ContentRootPath, "wwwroot\\posters", newFilm.Id);

                if (!Directory.Exists(upload))
                    Directory.CreateDirectory(upload);

                //string[] filePaths = Directory.GetFiles(upload);
                //foreach (string path in filePaths)
                //    System.IO.File.Delete(path);
                // временно убрал, позже доработать

                var filePath = Path.Combine(upload, file.FileName);

                // временная мера
                var localPath = "/posters/" + newFilm.Id + "/" + file.FileName;


                if (file.Length > 0)
                {
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                }

                if (newFilm != null)
                {
                    newFilm.PosterPath = localPath;
                }
            }

            if (newFilm != null)
            {
                newFilm.Name = film.Name;
                newFilm.About = film.About;
                newFilm.Year = film.Year;
                newFilm.Author = film.Author;
                db.Films.Update(newFilm);
            }
            db.SaveChanges();

            return RedirectToAction("Index");
        }

    }
}
