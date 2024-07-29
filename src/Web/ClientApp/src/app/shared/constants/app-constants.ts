export const CIRCLE_CONFIG = {
  DIAMETER: 30,
  HEADER: {
    MARGIN_RIGHT: 4,
  }
};

export const ICONS = {
  PATH: "/assets/icons/",
  MOBILE_NAVBAR_DEFAULT_SIZE: "30px",
  MOBILE_HEADER_DEFAULT_SIZE: "25px",
  FIXED_TOGGLE_BUTTONS_DEFAULT_SIZE: "23px"
};

export const WEB = {
  // Must be equal to $mobile-size (_variables.scss)
  MOBILE_SIZE: 768,
  // Must be equal to $pc-fixed-layout-grid (_variables.scss)
  PC_FIXED_LAYOUT: "max-content max-content auto 20%",
  PC_FIXED_LAYOUT_EXPAND: "pc-fixed-layout.expand",
  PC_FIXED_LAYOUT_COLLAPSE: "pc-fixed-layout.collapse",
  PC_SLIDES: 3,
  PC_CURRENT_LANG_SIZE: "18px",
  PC_LANG_SIZE: "20px",
  DEFAULT_LANG: "en",
  SUPPORTED_LANGUAGES: ['en', 'es'] as const
}

export const ABOUT_US = {
  SPONSOR: "https://github.com/sponsors/zetTtai",
  REPOSITORY: "https://github.com/zetTtai/DnDCharacterSheet",
  CONTACT: "raulbeltmarc@gmail.com"
}

export const EVENTS = {
  OPEN_MODAL: "openModal"
}

export const CURRENCY = {
  MIN: 0,
  MAX: 999
}

export const ABILITIES = {
  DEFAULT_VALUE: -1,
  VALUES: ['str', 'dex', 'con', 'int', 'wis', 'cha'],
  SCORE: {
    MIN: 3,
    MAX: 18,
  },
  MODIFIER_PREFIX: 'modifier',
  INFO: {
    CIRCLE: 20,
    ICON: '12px'
  },
  ABILITY: {
    CIRCLE: 30,
    ICON: '20px'
  }
}
