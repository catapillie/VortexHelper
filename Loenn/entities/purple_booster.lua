local utils = require "utils"

local purpleBooster = {}

purpleBooster.name = "VortexHelper/PurpleBooster"
purpleBooster.depth = -8500

purpleBooster.placements = {
    {
        name = "purple",
        data = {
            lavender = false,
            QoL = true,
            legacyGravityHelper = false,
            spriteOverride = ""
        }
    },
    {
        name = "lavender",
        data = {
            lavender = true,
            QoL = true,
            legacyGravityHelper = false
        }
    }
}

purpleBooster.fieldOrder = {
    "x", "y",
    "lavender", "QoL", "spriteOverride", "legacyGravityHelper"
}

function purpleBooster.texture(room, entity)
    return entity.lavender and "objects/VortexHelper/lavenderBooster/boosterLavender00"
                            or "objects/VortexHelper/slingBooster/slingBooster00"
end

function purpleBooster.selection(room, entity)
    local x, y = entity.x or 0, entity.y or 0
    return utils.rectangle(x - 9, y - 9, 18, 18)
end

return purpleBooster
