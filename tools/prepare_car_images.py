"""Download watermark-free Wikimedia photos and composite them on a DriveRent navy background."""
from __future__ import annotations

import io
import json
import time
import urllib.parse
import urllib.request
from pathlib import Path

from PIL import Image, ImageFilter, ImageOps
from rembg import new_session, remove

FORCE_FILES = {
    "toyota-corolla": "2021 Toyota Corolla LE, front right, 07-13-2024.jpg",
    "kia-cerato": "2021 Kia Forte GT, Front Left, 05-02-2021.jpg",
    "chevrolet-captiva": "2023 Chevrolet Captiva Premier.png",
    "hyundai-elantra": "2024 Hyundai Elantra Luxury in Atlas White, front left, 2024-08-11.jpg",
}

UA = "DriveRentCarRental/1.0 (educational student project; localhost)"
OUT_DIR = Path(r"d:\ITI Project\CarRentalSystem\wwwroot\images\cars")
SRC_DIR = Path(r"d:\ITI Project\tools\car-image-src")
TARGET_W, TARGET_H = 1600, 1000

CARS = [
    ("bmw-5-series", "BMW_5_Series_(G60)", "BMW 5 Series"),
    ("mercedes-c-class", "Mercedes-Benz_C-Class_(W206)", "Mercedes C-Class"),
    ("toyota-rav4", "Toyota_RAV4", "Toyota RAV4"),
    ("audi-a6", "Audi_A6", "Audi A6"),
    ("bmw-x5", "BMW_X5_(G05)", "BMW X5"),
    ("toyota-camry", "Toyota_Camry", "Toyota Camry"),
    ("toyota-corolla", "Toyota_Corolla", "Toyota Corolla"),
    ("toyota-yaris", "Toyota_Yaris_(XP210)", "Toyota Yaris"),
    ("toyota-land-cruiser", "Toyota_Land_Cruiser", "Toyota Land Cruiser"),
    ("bmw-3-series", "BMW_3_Series_(G20)", "BMW 3 Series"),
    ("bmw-x3", "BMW_X3", "BMW X3"),
    ("bmw-7-series", "BMW_7_Series_(G70)", "BMW 7 Series"),
    ("mercedes-e-class", "Mercedes-Benz_E-Class", "Mercedes E-Class"),
    ("mercedes-s-class", "Mercedes-Benz_S-Class_(W223)", "Mercedes S-Class"),
    ("mercedes-glc", "Mercedes-Benz_GLC", "Mercedes GLC"),
    ("mercedes-gle", "Mercedes-Benz_GLE", "Mercedes GLE"),
    ("audi-a3", "Audi_A3", "Audi A3"),
    ("audi-a4", "Audi_A4", "Audi A4"),
    ("audi-q3", "Audi_Q3", "Audi Q3"),
    ("audi-q5", "Audi_Q5", "Audi Q5"),
    ("hyundai-elantra", "Hyundai_Elantra", "Hyundai Elantra"),
    ("hyundai-sonata", "Hyundai_Sonata", "Hyundai Sonata"),
    ("hyundai-tucson", "Hyundai_Tucson", "Hyundai Tucson"),
    ("hyundai-santa-fe", "Hyundai_Santa_Fe", "Hyundai Santa Fe"),
    ("hyundai-accent", "Hyundai_Accent", "Hyundai Accent"),
    ("kia-cerato", "Kia_Forte", "Kia Cerato"),
    ("kia-sportage", "Kia_Sportage", "Kia Sportage"),
    ("kia-sorento", "Kia_Sorento", "Kia Sorento"),
    ("kia-k5", "Kia_K5", "Kia K5"),
    ("kia-picanto", "Kia_Picanto", "Kia Picanto"),
    ("ford-focus", "Ford_Focus", "Ford Focus"),
    ("ford-fusion", "Ford_Fusion_(Americas)", "Ford Fusion"),
    ("ford-mustang", "Ford_Mustang_(seventh_generation)", "Ford Mustang"),
    ("ford-explorer", "Ford_Explorer", "Ford Explorer"),
    ("ford-escape", "Ford_Escape", "Ford Escape"),
    ("honda-civic", "Honda_Civic", "Honda Civic"),
    ("honda-accord", "Honda_Accord", "Honda Accord"),
    ("honda-cr-v", "Honda_CR-V", "Honda CR-V"),
    ("honda-city", "Honda_City", "Honda City"),
    ("honda-hr-v", "Honda_HR-V", "Honda HR-V"),
    ("nissan-sunny", "Nissan_Almera", "Nissan Sunny"),
    ("nissan-sentra", "Nissan_Sentra", "Nissan Sentra"),
    ("nissan-altima", "Nissan_Altima", "Nissan Altima"),
    ("nissan-x-trail", "Nissan_X-Trail", "Nissan X-Trail"),
    ("nissan-patrol", "Nissan_Patrol", "Nissan Patrol"),
    ("chevrolet-malibu", "Chevrolet_Malibu", "Chevrolet Malibu"),
    ("chevrolet-cruze", "Chevrolet_Cruze", "Chevrolet Cruze"),
    ("chevrolet-tahoe", "Chevrolet_Tahoe", "Chevrolet Tahoe"),
    ("chevrolet-captiva", "Chevrolet_Captiva", "Chevrolet Captiva"),
    ("chevrolet-equinox", "Chevrolet_Equinox", "Chevrolet Equinox"),
]


def http_json(url: str) -> dict:
    req = urllib.request.Request(url, headers={"User-Agent": UA})
    with urllib.request.urlopen(req, timeout=60) as resp:
        return json.loads(resp.read().decode("utf-8"))


def http_bytes(url: str) -> bytes:
    req = urllib.request.Request(url, headers={"User-Agent": UA})
    with urllib.request.urlopen(req, timeout=90) as resp:
        return resp.read()


def wiki_thumbs(titles: list[str]) -> dict[str, dict]:
    encoded = "|".join(urllib.parse.quote(t, safe="()_") for t in titles)
    url = (
        "https://en.wikipedia.org/w/api.php?action=query&prop=pageimages"
        "&format=json&pithumbsize=1920&piprop=thumbnail|name&redirects=1"
        f"&titles={encoded}"
    )
    data = http_json(url)
    pages = data.get("query", {}).get("pages", {})
    redirects = {
        item["from"]: item["to"]
        for item in data.get("query", {}).get("redirects", [])
    }
    normalized = {
        item["from"]: item["to"]
        for item in data.get("query", {}).get("normalized", [])
    }
    by_title = {}
    for page in pages.values():
        by_title[page.get("title", "")] = page
    result = {}
    for title in titles:
        lookup = title.replace("_", " ")
        lookup = normalized.get(lookup, lookup)
        lookup = redirects.get(lookup, lookup)
        result[title] = by_title.get(lookup, {})
    return result


def commons_file_thumb(filename: str) -> str | None:
    url = (
        "https://commons.wikimedia.org/w/api.php?action=query&prop=imageinfo"
        "&iiprop=url&iiurlwidth=1920&format=json"
        f"&titles={urllib.parse.quote('File:' + filename)}"
    )
    data = http_json(url)
    for page in data.get("query", {}).get("pages", {}).values():
        infos = page.get("imageinfo") or []
        if infos:
            return infos[0].get("thumburl") or infos[0].get("url")
    return None


def commons_search(query: str) -> str | None:
    url = (
        "https://commons.wikimedia.org/w/api.php?action=query&list=search"
        "&srnamespace=6&srlimit=8&format=json"
        f"&srsearch={urllib.parse.quote(query)}"
    )
    data = http_json(url)
    hits = data.get("query", {}).get("search", [])
    skip = ("logo", "badge", "interior", "engine", "dashboard", "seat", "wheel")
    for hit in hits:
        title = hit.get("title", "")
        low = title.lower()
        if any(word in low for word in skip):
            continue
        file_title = title.replace("File:", "")
        info_url = (
            "https://commons.wikimedia.org/w/api.php?action=query&prop=imageinfo"
            "&iiprop=url&iiurlwidth=1920&format=json"
            f"&titles={urllib.parse.quote('File:' + file_title)}"
        )
        info = http_json(info_url)
        pages = info.get("query", {}).get("pages", {})
        for page in pages.values():
            infos = page.get("imageinfo") or []
            if infos:
                return infos[0].get("thumburl") or infos[0].get("url")
    return None


def make_background() -> Image.Image:
    import numpy as np

    yy, xx = np.indices((TARGET_H, TARGET_W))
    cx, cy = TARGET_W * 0.48, TARGET_H * 0.42
    maxd = ((TARGET_W ** 2 + TARGET_H ** 2) ** 0.5) * 0.62
    t = np.minimum(1.0, np.sqrt((xx - cx) ** 2 + (yy - cy) ** 2) / maxd) ** 1.15
    inner = np.array([28, 58, 98], dtype=np.float32)
    outer = np.array([6, 18, 34], dtype=np.float32)
    rgb = (inner + (outer - inner) * t[..., None]).astype(np.uint8)
    return Image.fromarray(rgb, "RGB")


def trim_alpha(im: Image.Image, threshold: int = 12) -> Image.Image:
    if im.mode != "RGBA":
        im = im.convert("RGBA")
    alpha = im.split()[-1]
    mask = alpha.point(lambda a: 255 if a > threshold else 0)
    bbox = mask.getbbox()
    if not bbox:
        return im
    return im.crop(bbox)


def composite_car(cutout: Image.Image, background: Image.Image) -> Image.Image:
    car = trim_alpha(cutout)
    max_w = int(TARGET_W * 0.86)
    max_h = int(TARGET_H * 0.72)
    ratio = min(max_w / car.width, max_h / car.height)
    new_size = (max(1, int(car.width * ratio)), max(1, int(car.height * ratio)))
    car = car.resize(new_size, Image.Resampling.LANCZOS)

    canvas = background.copy()
    x = (TARGET_W - car.width) // 2
    y = TARGET_H - car.height - int(TARGET_H * 0.08)

    shadow = Image.new("RGBA", (TARGET_W, TARGET_H), (0, 0, 0, 0))
    shadow_layer = Image.new("RGBA", car.size, (0, 0, 0, 0))
    alpha = car.split()[-1].point(lambda a: int(a * 0.45) if a > 20 else 0)
    shadow_layer.putalpha(alpha)
    shadow.paste(shadow_layer, (x, y + 18), shadow_layer)
    shadow = shadow.filter(ImageFilter.GaussianBlur(18))
    canvas = Image.alpha_composite(canvas.convert("RGBA"), shadow)
    canvas.paste(car, (x, y), car)
    return canvas.convert("RGB")


def process_one(slug: str, wiki: str, label: str, thumbs: dict, background: Image.Image, session) -> None:
    page = thumbs.get(wiki, {})
    thumb = None
    forced = slug in FORCE_FILES
    if slug in FORCE_FILES:
        thumb = commons_file_thumb(FORCE_FILES[slug])
    if not thumb:
        thumb = (page.get("thumbnail") or {}).get("source")
    pageimage = (page.get("pageimage") or "").lower()
    if not thumb:
        found = commons_search(f"{label} front")
        if found:
            thumb = found
    if not thumb:
        raise RuntimeError(f"No source image for {slug}")

    SRC_DIR.mkdir(parents=True, exist_ok=True)
    raw_path = SRC_DIR / f"{slug}-raw.bin"
    raw = http_bytes(thumb)
    raw_path.write_bytes(raw)

    src = Image.open(io.BytesIO(raw))
    src = ImageOps.exif_transpose(src)
    src = src.convert("RGBA")
    cutout = remove(src, session=session)
    if not isinstance(cutout, Image.Image):
        cutout = Image.open(io.BytesIO(cutout)).convert("RGBA")
    final = composite_car(cutout, background)

    OUT_DIR.mkdir(parents=True, exist_ok=True)
    out_path = OUT_DIR / f"{slug}.jpg"
    final.save(out_path, "JPEG", quality=90, optimize=True)
    webp = OUT_DIR / f"{slug}.webp"
    if webp.exists():
        webp.unlink()
    print(f"OK {slug} <= {page.get('pageimage', thumb)}")


def main(only: list[str] | None = None) -> None:
    selected = [item for item in CARS if not only or item[0] in only]
    titles = [wiki for _, wiki, _ in selected]
    thumbs = wiki_thumbs(titles)
    print("Loading u2net session...")
    session = new_session("u2net")
    print("Building shared background...")
    background = make_background()
    for slug, wiki, label in selected:
        process_one(slug, wiki, label, thumbs, background, session)
        time.sleep(0.15)


if __name__ == "__main__":
    import sys
    main(sys.argv[1:] or None)
