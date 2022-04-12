﻿namespace Saas.SignupAdministration.Web.Areas.Admin.Controllers;

[Area("Admin")]
public class TenantsController : Controller
{
    private readonly IAdminServiceClient _adminServiceClient;

    public TenantsController(IAdminServiceClient adminServiceClient)
    {
        _adminServiceClient = adminServiceClient;
    }

    // GET: Admin/Tenants
    public async Task<IActionResult> Index()
    {
        return View(await _adminServiceClient.TenantsAllAsync());
    }

    // GET: Admin/Tenants/Details/5
    public async Task<IActionResult> Details(string id)
    {
        Guid guid = new Guid();
        if (id == null || !Guid.TryParse(id, out guid))
        {
            return NotFound();
        }

        var tenant = await _adminServiceClient.TenantsGETAsync(guid);
        if (tenant == null)
        {
            return NotFound();
        }

        return View(tenant);
    }

    // GET: Admin/Tenants/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Admin/Tenants/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Name, RoutePrefix")] NewTenantRequest newTenantRequest)
    {
        if (ModelState.IsValid)
        {
            await _adminServiceClient.TenantsPOSTAsync(newTenantRequest);
            return RedirectToAction(nameof(Index), nameof(TenantsController));
        }
        return View(newTenantRequest);
    }

    // GET: Admin/Tenants/Edit/5
    public async Task<IActionResult> Edit(string id)
    {
        Guid guid = new Guid();
        if (id == null || !Guid.TryParse(id, out guid))
        {
            return NotFound();
        }

        var tenant = await _adminServiceClient.TenantsGETAsync(guid);
        if (tenant == null)
        {
            return NotFound();
        }
        return View(tenant);
    }

    // POST: Admin/Tenants/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(string id, [Bind("Id,Name,IsCancelled,IsProvisioned,RoutePrefix")] TenantDTO tenant)
    {
        Guid guid = new Guid();
        if (!Guid.TryParse(id, out guid) || guid != tenant.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                await _adminServiceClient.TenantsPUTAsync(guid, tenant);
            }
            catch (ApiException ex)
            {
                return NotFound();
            }
            return RedirectToAction(nameof(Index));
        }
        return View(tenant);
    }

    // GET: Admin/Tenants/Delete/5
    public async Task<IActionResult> Delete(string id)
    {
        Guid guid = new Guid();
        if (id == null || !Guid.TryParse(id, out guid))
        {
            return NotFound();
        }

        var tenant = await _adminServiceClient.TenantsGETAsync(guid);
        if (tenant == null)
        {
            return NotFound();
        }

        return View(tenant);
    }

    // POST: Admin/Tenants/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(string id)
    {
        Guid guid = new Guid();
        if (id == null || !Guid.TryParse(id, out guid))
        {
            return NotFound();
        }
        await _adminServiceClient.TenantsDELETEAsync(guid);
        return RedirectToAction(nameof(Index));
    }
}