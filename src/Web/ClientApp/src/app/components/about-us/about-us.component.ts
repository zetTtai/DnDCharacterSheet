import { Component } from '@angular/core';
import { IconInfo } from 'src/app/shared/models/icon-info.model';
import { MemberInfo } from 'src/app/shared/models/member-info.model';
import { ABOUT_US } from 'src/app/shared/constants/app-constants';

@Component({
  selector: 'app-about-us',
  templateUrl: './about-us.component.html',
  styleUrl: './about-us.component.scss'
})
export class AboutUsComponent {
  public sponsor_url = ABOUT_US.SPONSOR;
  public repository_url = ABOUT_US.REPOSITORY;
  public contact = ABOUT_US.CONTACT;

  public icons: IconInfo[]= [
    {
      "name": "minus-outline",
      "url": "https://icon-sets.iconify.design/typcn/minus-outline/",
      "author": "Stephen Hutchings",
      "license_url": "https://creativecommons.org/licenses/by-sa/4.0/",
      "license_name": "CC BY-SA 4.0"
    },
    {
      "name": "abilities",
      "url": "https://icon-sets.iconify.design/game-icons/spinning-sword/",
      "author": "GameIcons",
      "license_url": "https://github.com/game-icons/icons/blob/master/license.txt",
      "license_name": "CC BY 3.0"
    },
    {
      "name": "death-saves",
      "url": "https://icon-sets.iconify.design/majesticons/skull-line/",
      "author": "Gerrit Halfmann",
      "license_url": "https://github.com/halfmage/majesticons/blob/main/LICENSE",
      "license_name": "MIT"
    },
    {
      "name": "features-feats",
      "url": "https://icon-sets.iconify.design/mdi/feature-highlight/",
      "author": "Pictogrammers",
      "license_url": "https://github.com/Templarian/MaterialDesign/blob/master/LICENSE",
      "license_name": "Apache 2.0"
    },
    {
      "name": "info/info-circle",
      "url": "https://icon-sets.iconify.design/simple-line-icons/info/",
      "author": "Sabbir Ahmed",
      "license_url": "https://github.com/thesabbir/simple-line-icons/blob/master/LICENSE.md",
      "license_name": "MIT"
    },
    {
      "name": "mobile-account",
      "url": "https://icon-sets.iconify.design/mdi/account/",
      "author": "Pictogrammers",
      "license_url": "https://github.com/Templarian/MaterialDesign/blob/master/LICENSE",
      "license_name": "Apache 2.0"
    },
    {
      "name": "mobile-home",
      "url": "https://icon-sets.iconify.design/mdi/castle/",
      "author": "Pictogrammers",
      "license_url": "https://github.com/Templarian/MaterialDesign/blob/master/LICENSE",
      "license_name": "Apache 2.0"
    },
    {
      "name": "mobile-items",
      "url": "https://icon-sets.iconify.design/ph/treasure-chest/",
      "author": "Phosphor Icons",
      "license_url": "https://github.com/phosphor-icons/core/blob/main/LICENSE",
      "license_name": "MIT"
    },
    {
      "name": "mobile-lore",
      "url": "https://icon-sets.iconify.design/ph/books-duotone/",
      "author": "Phosphor Icons",
      "license_url": "https://github.com/phosphor-icons/core/blob/main/LICENSE",
      "license_name": "MIT"
    },
    {
      "name": "mobile-spells",
      "url": "https://icon-sets.iconify.design/game-icons/spell-book/",
      "author": "GameIcons",
      "license_url": "https://github.com/game-icons/icons/blob/master/license.txt",
      "license_name": "CC BY 3.0"
    },
    {
      "name": "notes",
      "url": "https://icon-sets.iconify.design/mdi/notebook-outline/",
      "author": "Pictogrammers",
      "license_url": "https://github.com/Templarian/MaterialDesign/blob/master/LICENSE",
      "license_name": "Apache 2.0"
    },
    {
      "name": "plus-outline",
      "url": "https://icon-sets.iconify.design/mdi/plus-outline/",
      "author": "Pictogrammers",
      "license_url": "https://github.com/Templarian/MaterialDesign/blob/master/LICENSE",
      "license_name": "Apache 2.0"
    },
    {
      "name": "wallet",
      "url": "https://icon-sets.iconify.design/game-icons/two-coins/",
      "author": "GameIcons",
      "license_url": "https://github.com/game-icons/icons/blob/master/license.txt",
      "license_name": "CC BY 3.0"
    },
    {
      "name": "lang/es",
      "url": "https://icon-sets.iconify.design/game-icons/two-coins/",
      "author": "GameIcons",
      "license_url": "https://github.com/game-icons/icons/blob/master/license.txt",
      "license_name": "CC BY 3.0"
    },
    {
      "name": "lang/en",
      "url": "https://icon-sets.iconify.design/game-icons/two-coins/",
      "author": "GameIcons",
      "license_url": "https://github.com/game-icons/icons/blob/master/license.txt",
      "license_name": "CC BY 3.0"
    },
    {
      "name": "abilities/_str",
      "url": "https://icon-sets.iconify.design/lucide/biceps-flexed/",
      "author": "Lucide Contributors",
      "license_url": "https://github.com/lucide-icons/lucide/blob/main/LICENSE",
      "license_name": "ISC"
    },
    {
      "name": "abilities/_dex",
      "url": "https://icon-sets.iconify.design/mdi/sword-fight/",
      "author": "Pictogrammers",
      "license_url": "https://github.com/Templarian/MaterialDesign/blob/master/LICENSE",
      "license_name": "Apache 2.0"
    },
    {
      "name": "abilities/_con",
      "url": "https://icon-sets.iconify.design/material-symbols-light/ecg-heart-sharp/",
      "author": "Google",
      "license_url": "https://github.com/google/material-design-icons/blob/master/LICENSE",
      "license_name": "Apache 2.0"
    },
    {
      "name": "abilities/_int",
      "url": "https://icon-sets.iconify.design/hugeicons/brain/",
      "author": "Hugeicons",
      "license_url": "",
      "license_name": "MIT"
    },
    {
      "name": "abilities/_wis",
      "url": "https://icon-sets.iconify.design/game-icons/owl/",
      "author": "GameIcons",
      "license_url": "https://github.com/game-icons/icons/blob/master/license.txt",
      "license_name": "CC BY 3.0"
    },
    {
      "name": "abilities/_cha",
      "url": "https://icon-sets.iconify.design/fa-solid/theater-masks/",
      "author": "Dave Gandy",
      "license_url": "https://creativecommons.org/licenses/by/4.0/",
      "license_name": "CC BY 4.0"
    },
  ];


  public members: MemberInfo[] = [
    {
      name: "Raúl Beltrán Marco",
      position: "Project Manager",
      image: "assets/images/members/raul.JPG",
      networks: [
        {
          name: "github",
          url: "https://github.com/zetTtai"
        },
        {
          name: "linkedin",
          url: "https://www.linkedin.com/in/raul-beltran-marco-a10a52236/"
        }
      ]
    },
    {
      name: "Gabriel de Lamo Dutra",
      position: "Cucurella promedio",
      image: "",
      networks: [
        {
          name: "github",
          url: "https://github.com/zetTtai"
        },
        {
          name: "linkedin",
          url: "https://www.linkedin.com/in/gabriel-de-lamo-dutra-63232a147/"
        }
      ]
    },
    {
      name: "Raúl Beltrán Marco",
      position: "Full-Stack Developer (Leader)",
      image: "assets/images/members/raul.JPG",
      networks: [
        {
          name: "github",
          url: "https://github.com/zetTtai"
        },
        {
          name: "linkedin",
          url: "https://www.linkedin.com/in/raul-beltran-marco-a10a52236/"
        }
      ]
    },
    {
      name: "Raúl Beltrán Marco",
      position: "UI/UX Designer",
      image: "assets/images/members/raul.JPG",
      networks: [
        {
          name: "github",
          url: "https://github.com/zetTtai"
        },
        {
          name: "linkedin",
          url: "https://www.linkedin.com/in/raul-beltran-marco-a10a52236/"
        }
      ]
    },
    {
      name: "Raúl Beltrán Marco",
      position: "QA Tester/Tester",
      image: "assets/images/members/raul.JPG",
      networks: [
        {
          name: "github",
          url: "https://github.com/zetTtai"
        },
        {
          name: "linkedin",
          url: "https://www.linkedin.com/in/raul-beltran-marco-a10a52236/"
        }
      ]
    },
    {
      name: "Raúl Beltrán Marco",
      position: "DevOps Engineer",
      image: "assets/images/members/raul.JPG",
      networks: [
        {
          name: "github",
          url: "https://github.com/zetTtai"
        },
        {
          name: "linkedin",
          url: "https://www.linkedin.com/in/raul-beltran-marco-a10a52236/"
        }
      ]
    },
    {
      name: "Raúl Beltrán Marco",
      position: "Database Administrator",
      image: "assets/images/members/raul.JPG",
      networks: [
        {
          name: "github",
          url: "https://github.com/zetTtai"
        },
        {
          name: "linkedin",
          url: "https://www.linkedin.com/in/raul-beltran-marco-a10a52236/"
        }
      ]
    },
    {
      name: "Raúl Beltrán Marco",
      position: "Business Analyst",
      image: "assets/images/members/raul.JPG",
      networks: [
        {
          name: "github",
          url: "https://github.com/zetTtai"
        },
        {
          name: "linkedin",
          url: "https://www.linkedin.com/in/raul-beltran-marco-a10a52236/"
        }
      ]
    },
    {
      name: "Raúl Beltrán Marco",
      position: "Business Analyst",
      image: "assets/images/members/raul.JPG",
      networks: [
        {
          name: "github",
          url: "https://github.com/zetTtai"
        },
        {
          name: "linkedin",
          url: "https://www.linkedin.com/in/raul-beltran-marco-a10a52236/"
        }
      ]
    },
    {
      name: "Raúl Beltrán Marco",
      position: "Technical Writer",
      image: "assets/images/members/raul.JPG",
      networks: [
        {
          name: "github",
          url: "https://github.com/zetTtai"
        },
        {
          name: "linkedin",
          url: "https://www.linkedin.com/in/raul-beltran-marco-a10a52236/"
        }
      ]
    },
  ]
}
