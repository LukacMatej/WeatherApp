﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OpenMeteo;
using WeatherApp.Data;
using WeatherApp.Models;

namespace WeatherApp.Controllers
{
    public class WeathersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public WeathersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Weathers
        public async Task<IActionResult> Index()
        {
            return View(await _context.Weather.ToListAsync());
        }

        // GET: Weathers/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var weather = await _context.Weather
                .FirstOrDefaultAsync(m => m.City == id);
            if (weather == null)
            {
                return NotFound();
            }

            return View(weather);
        }
        public IActionResult ShowSearchForm()
        {
            return View();
        }
        public async Task<IActionResult> ShowSearchResult(String City)
        {
            if (_context.Weather.Any(w => w.City == City))
            {
                return RedirectToAction("Details", new { id = City });
            }
            else
            {
                OpenMeteo.OpenMeteoClient client = new OpenMeteo.OpenMeteoClient();
                WeatherForecast weatherData = await client.QueryAsync(City);
                Weather weather = new Weather();
                weather.City = City;
                weather.Humidity = weatherData.Current.Rain;
                weather.Temp = weatherData.Current.Temperature;
                weather.TempFeelsLike = weatherData.Current.Apparent_temperature;
                weather.Time = weatherData.Current.Time;
                weather.WindSpeed = weatherData.Current.Windspeed_10m;
                weather.WindDir = weatherData.Current.Winddirection_10m;
                if (ModelState.IsValid)
                {
                    _context.Add(weather);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Details", new { id = City });
                }
                return View(weather);
            }
        }
        // GET: Weathers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Weathers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("City,Lat,Lon,Time,Humidity,TempFeelsLike,Temp,WindSpeed,WindDir,WeatherIcon")] Weather weather)
        {
            if (ModelState.IsValid)
            {
                _context.Add(weather);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(weather);
        }

        // GET: Weathers/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var weather = await _context.Weather.FindAsync(id);
            if (weather == null)
            {
                return NotFound();
            }
            return View(weather);
        }

        // POST: Weathers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("City,Country,Lat,Lon,Description,Humidity,TempFeelsLike,Temp,TempMax,TempMin,WeatherIcon")] Weather weather)
        {
            if (id != weather.City)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(weather);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WeatherExists(weather.City))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(weather);
        }

        // GET: Weathers/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var weather = await _context.Weather
                .FirstOrDefaultAsync(m => m.City == id);
            if (weather == null)
            {
                return NotFound();
            }

            return View(weather);
        }

        // POST: Weathers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var weather = await _context.Weather.FindAsync(id);
            if (weather != null)
            {
                _context.Weather.Remove(weather);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WeatherExists(string id)
        {
            return _context.Weather.Any(e => e.City == id);
        }
    }
}
