UPDATE import1_users
SET lastenter =
    CASE
        WHEN TRY_CONVERT(DATE, lastenter, 101) IS NOT NULL
        THEN FORMAT(TRY_CONVERT(DATE, lastenter, 101), 'dd.MM.yyyy')
        ELSE lastenter
    END

