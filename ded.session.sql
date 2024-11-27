-- SELECT * 
-- FROM (
--     SELECT 
--         SumTraverseProducts.FinalProduct, 
--         SumTraverseProducts.ChildNode, 
--         SumTraverseProducts.SumQuantityComponents, 
--         SumTraverseProducts.MaxNumericLevel, 
--         Nomenclatures.TypeId
--     FROM 
--         SumTraverseProducts
--     INNER JOIN 
--         Nomenclatures 
--     ON 
--         SumTraverseProducts.ChildNode = Nomenclatures.Id
-- ) AS SubQuery
-- WHERE SubQuery.TypeId = 2 OR SubQuery.TypeId = 3;

-- SELECT 
--     tn.CdVyr,                           -- Production code (e.g., A, M)
--     rm.CdRm,                            -- Workstation code
--     rm.NmRm,                            -- Workstation name
--     rm.VrtGod,                          -- Value of God (VrtGod)
--     SUM(tn.SumGodin) AS SumRmGod,        -- Total SumGodin for each group
--     SUM(rm.VrtGod * tn.SumGodin) AS SumVrt -- Sum of VrtGod multiplied by SumGodin (SumVrt)
-- FROM 
--     TechnNorm tn
-- JOIN 
--     TO_RM tr ON tn.CdTO = tr.CdTO        -- Join TechnNorm and TO_RM on CdTO
-- JOIN 
--     RABM rm ON tr.CdRm = rm.CdRm         -- Join TO_RM and RABM on CdRm
-- GROUP BY 
--     tn.CdVyr,                            -- Group by CdVyr
--     rm.CdRm,                             -- Group by CdRm
--     rm.NmRm,                             -- Include the workstation name for display
--     rm.VrtGod                             -- Include VrtGod for multiplication calculation
-- ORDER BY 
--     tn.CdVyr, rm.CdRm;                   -- Optional: Order by CdVyr and CdRm

-- Create the CapNorm table
-- CREATE TABLE CapNorm (
--     CdVyr VARCHAR(1),
--     CdRm INT,
--     NmRm VARCHAR(255),
--     VrtGod DECIMAL(10, 2),
--     SumRmGod DECIMAL(10, 2),
--     SumVrt DECIMAL(10, 2)
-- );

-- -- Insert data into the CapNorm table
-- INSERT INTO CapNorm (CdVyr, CdRm, NmRm, VrtGod, SumRmGod, SumVrt) VALUES
-- ('A', 1, 'Токарно-винторезный станок', 18.60, 80.40, 1495.44),
-- ('A', 3, 'Радиально-сверлильный станок', 19.30, 145.60, 2810.08),
-- ('A', 20, 'Пресс кривошипный (2,5т.)', 19.30, 18.60, 358.98),
-- ('A', 24, 'Пресс гидравличкский (63т.)', 20.80, 95.76, 1991.81),
-- ('A', 29, 'Пресс-ножницы', 21.47, 16.12, 346.10),
-- ('A', 59, 'Рабочее место механосборщика', 16.45, 5.89, 96.89),
-- ('A', 61, 'Стенд для сборки электрооборудования', 17.50, 7.58, 132.65),
-- ('A', 63, 'Испытательный стенд електрооборудования', 28.00, 0.93, 26.04),
-- ('A', 64, 'Рабочее место електромонтажа', 13.00, 11.16, 145.08),
-- ('A', 76, 'Установка для нанесения лакокрасочный покрытий', 28.00, 1.82, 50.96),
-- ('A', 78, 'Установка для покраски мелкогабаритних изделий', 18.28, 0.93, 17.00),
-- ('M', 1, 'Токарно-винторезный станок', 18.60, 26.80, 498.48),
-- ('M', 3, 'Радиально-сверлильный станок', 19.30, 50.60, 976.58),
-- ('M', 20, 'Пресс кривошипный (2,5т.)', 19.30, 8.68, 167.52),
-- ('M', 24, 'Пресс гидравличкский (63т.)', 20.80, 31.92, 663.94),
-- ('M', 29, 'Пресс-ножницы', 21.47, 7.44, 159.74),
-- ('M', 59, 'Рабочее место механосборщика', 16.45, 3.72, 61.19),
-- ('M', 61, 'Стенд для сборки электрооборудования', 17.50, 2.80, 49.00),
-- ('M', 64, 'Рабочее место електромонтажа', 13.00, 6.64, 86.32);

select * from WorkerNorm;


 

 